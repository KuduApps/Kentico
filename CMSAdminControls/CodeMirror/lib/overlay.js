// Utility function that allows modes to be combined. The mode given
// as the base argument takes care of most of the normal mode
// functionality, but a second (typically simple) mode is used, which
// can override the style of text. Both modes get to parse all of the
// text, but when both assign a non-null style to a piece of code, the
// overlay wins, unless the combine argument was true, in which case
// the styles are combined.

CodeMirror.overlayParser = function(base, overlay, combine) {
    return {
        startState: function() {
            return {
                base: CodeMirror.startState(base),
                overlay: CodeMirror.startState(overlay),
                basePos: 0, baseCur: null,
                overlayPos: 0, overlayCur: null
            };
        },
        copyState: function(state) {
            return {
                base: CodeMirror.copyState(base, state.base),
                overlay: CodeMirror.copyState(overlay, state.overlay),
                basePos: state.basePos, baseCur: null,
                overlayPos: state.overlayPos, overlayCur: null
            };
        },

        token: function(stream, state) {
            if (stream.start == state.basePos) {
                state.baseCur = base.token(stream, state.base);
                state.basePos = stream.pos;
            }
            if (stream.start == state.overlayPos) {
                stream.pos = stream.start;
                state.overlayCur = overlay.token(stream, state.overlay);
                state.overlayPos = stream.pos;
            }
            stream.pos = Math.min(state.basePos, state.overlayPos);
            if (stream.eol()) state.basePos = state.overlayPos = 0;

            if (state.overlayCur == null) return state.baseCur;
            if (state.baseCur != null && combine) return state.baseCur + " " + state.overlayCur;
            else return state.overlayCur;
        },

        indent: function(state, textAfter) {
            if (base.indent) {
                return base.indent(state.base, textAfter);
            }
        },
        electricChars: base.electricChars
    };
};

/* CMS */
CodeMirror.RegisterMacroOverlay = function(mode, mime) {
    CodeMirror.defineMode(mode + '_macro', function(config, parserConfig) {
        var origName = mode;
        var origMode = CodeMirror.getMode(config, mime || origName);
        var clikeMode = CodeMirror.getMode(config, "text/x-csharp");
        var macroRegEx = new RegExp("^##[a-zA-Z]+##");

        function original(stream, state) {
            if ((origName == "htmlmixed") || (origName == "aspnet")) {
                if (stream.match('"{%', false) || stream.match("'{%", false)) {
                    this.quote = stream.next();
                    return "string";
                }
            }

            if (stream.match(/^{(\([0-9]+\))?[%$^@?#]/, true)) {
                var current = stream.current();
                var type = current.substr(current.length - 1, 1);
                state.macroEnd = type + current.substring(1, current.length - 1) + "}";
                if (type == '%') {
                    state.token = clike;
                    state.localState = clikeMode.startState();
                    state.mode = "clike";
                }
                else {
                    state.token = inMacro;
                }
                return "macro";
            }

            if (stream.match(macroRegEx, false)) {
                stream.next();
                stream.next();
                state.token = macro;
                return "macro";
            }

            return origMode.token(stream, state.origState);
        }

        function maybeBackup(stream, pat, style) {
            var cur = stream.current();
            var close = cur.search(pat);
            if (close > -1) stream.backUp(cur.length - close);
            return style;
        }

        function untilQuote(stream, state) {
            var ch = stream.next()
            while (ch != this.quote) {
                if ((ch = stream.next()) == null) break;
            }
            state.token = original;
            state.localState = null;
            state.mode = origName;
            this.quote = null;

            return "string";
        }

        function inMacro(stream, state) {
            var macroEnd = state.macroEnd;
            if (stream.match(macroEnd, true)) {
                state.token = original;
                return "macro";
            }

            while (!stream.match(macroEnd, false)) {
                if (stream.next() == null) break;
            }

            return "mc";
        }

        function clike(stream, state) {
            var macroEnd = state.macroEnd;
            if (stream.match(macroEnd, true)) {
                if (this.quote != null) {
                    state.token = untilQuote;
                }
                else {
                    state.token = original;
                }
                state.localState = null;
                state.mode = origName;
                return "macro";
            }

            if (stream.match(/^\|\([a-z]+\)/i, true)) {
                while (!stream.match(macroEnd, false) && stream.next());
                return "params cm-mc";
            }

            return maybeBackup(stream, /\%\}/,
                       clikeMode.token(stream, state.localState)) + " cm-mc";
        }

        function macro(stream, state) {
            if (stream.match('##', true)) {
                state.token = original;
                return "macro";
            }

            while (!stream.match('##', false)) {
                if (stream.next() == null) break;
            }

            return "mc";
        }

        return {
            startState: function() {
                var state = (origMode.startState ? origMode.startState() : null);
                return { token: original, localState: null, mode: origName, origState: state };
            },

            copyState: function(state) {
                if (state.localState)
                    var local = CodeMirror.copyState(state.token == clike ? clikeMode : origName, state.localState);
                return { token: state.token, localState: local, mode: state.mode,
                    origState: CodeMirror.copyState(origMode, state.origState)
                };
            },

            token: function(stream, state) {
                return state.token(stream, state);
            }
        }
    });
}

CodeMirror.RegisterMacroOverlay("css", "text/css");
CodeMirror.RegisterMacroOverlay("htmlmixed");
CodeMirror.RegisterMacroOverlay("aspnet");
CodeMirror.RegisterMacroOverlay("clike", "text/x-csharp");
CodeMirror.RegisterMacroOverlay("xml", "application/xml");
CodeMirror.RegisterMacroOverlay("plsql", "text/x-plsql");
CodeMirror.RegisterMacroOverlay("javascript");
/* CMS end */
