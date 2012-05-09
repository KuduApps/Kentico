// KeyCode constants
var TAB = 9;
var ENTER = 13;
var ESC = 27;
var PGUP = 33;
var PGDOWN = 34;
var HOME = 35;
var END = 36;
var KEYUP = 38;
var KEYDOWN = 40;
var KEYLEFT = 37;
var KEYRIGHT = 39;
var CTRL = 17;
var BACKSPACE = 8;
var DELETE = 46;

// CharCode constants
var SPACE = 32;
var PERCENT = 37;
var COMMA = 44;
var DOT = 46;
var DQUOTE = 58;
var SEMICOLON = 59;
var LEFTBRACKET = 40;
var RIGHTBRACKET = 41;
var LEFTINDEXER = 91;
var RIGHTINDEXER = 93;
var RIGHTCURLY = 125;

// Regex
var ALPHANUMERIC_REGEX = '[a-zA-Z0-9_]';
var MAX_ZINDEX = 2147483647;

// Methods comments hashtable
var methodComments = new Object();

function AutoCompleteExtender(codeElem, hintElem, contextElem, quickContextElem, mixedMode, editorTopOffset, editorLeftOffset) {

    // The 'me' variable allow you to access the AutoSuggest object
    // from the elem's event handlers defined below.
    var me = this;

    // If true, auto completion is always shown above the cursor
    this.forceAbove = false;

    // Save editor offset
    this.topOffset = editorTopOffset;
    this.leftOffset = editorLeftOffset;

    // A reference to the element we're binding the autocompletion to
    this.elem = codeElem;

    if (this.elem.lineContent == null) {
        this.elem.lineContent = this.elem.getLine;
    }
    if (this.elem.cursorLine == null) {
        this.elem.cursorLine = function() { return this.getCursor().line; }
    }
    if (this.elem.frame == null) {
        this.elem.frame = this.elem.getInputField();
    }

    // Indicates whether the editor is in pure macro editing mode (whole text is considered as macro) 
    // or mixed mode, where auto completion is fired only inside {%%} environment.
    this.isMixedMode = mixedMode;

    // A div to use to create the dropdown.
    this.hintsDiv = hintElem;
    this.hintsDiv.style.zIndex = MAX_ZINDEX;

    // A div to use to display method context help
    this.contextDiv = contextElem;
    this.contextDiv.style.zIndex = MAX_ZINDEX;

    // A div to use to display quick method context help (when browsing through hints)
    this.quickContextDiv = quickContextElem;
    this.quickContextDiv.style.zIndex = MAX_ZINDEX;

    // Array storing relevant hints (filled with callback data)
    this.hints = new Array();

    // Field storing the current context (filled with callback data)
    this.context = '';

    // Parameters of lexem by the caret (filled with callback data)
    this.currentElementPosition;
    this.currentElementLenght;

    // Field used to pass arguments to callback functions
    this.callbackArgument = null;

    // Flag for CTRL+SPACE if there is only one hint, it will be used automatically
    this.autoComplete = false;

    // If true, next normal key (alphanumeric) will cause the hints to be displayed
    this.showHintNextTime = true;

    // A pointer to the index of the highlighted eligible item. -1 means nothing highlighted.
    this.highlighted = -1;

    // This falg is to ensure that lost focus because of clicking the option from hint with mouse does not cause the hint to hide too early
    this.stillHasFocus = false;

    // Register the events
    if (this.elem.win != null) {
        this.docObj = jQuery(this.elem.win.document);
    }
    else {
        this.docObj = jQuery(this.elem.getInputField());
    }

    this.docObj.keydown(function(e) {
        me.handleKeyDown(e);
    });
    this.docObj.keyup(function(e) {
        me.handleKeyUp(e);
    });
    this.docObj.keypress(function(e) {
        me.handleKeyPress(e);
    });
    this.docObj.mouseup(function(e) {
        me.handleMouseUp(e);
    });

    // Events that handle hiding of the autocompletion when the focus is outside the editor
    this.frameObj = jQuery(this.elem.frame);
    this.frameObj.blur(function(e) {
        if (!me.stillHasFocus) {
            me.hideAutoCompletion();
            me.editorFocusLost();
        }
    });
    this.docObj.blur(function(e) {
        if (!me.stillHasFocus) {
            me.hideAutoCompletion();
            me.editorFocusLost();
        }
    });

    this.scrollerObj = jQuery(this.elem.getScrollerElement);
    this.scrollerObj.mouseup(function(e) {
        me.handleMouseUp(e);
    });

    this.hintsObj = jQuery(this.hintsDiv);
    this.hintsObj.mouseover(function(e) {
        me.stillHasFocus = true;
    });
    this.hintsObj.mouseout(function(e) {
        me.stillHasFocus = false;
    });


    /*
    * Keyboard handlers.
    */

    // Indicator variable
    this.handleKeyDown = function(ev) {
        var key = me.getKeyCode(ev);
        switch (key) {
            case END:
            case HOME:
            case ESC:
                {
                    me.hideHints(false);
                    me.hideContext();
                    me.hideQuickContext();
                }
                break;

            case KEYLEFT:
            case KEYRIGHT:
                if (this.autoCompleteEnabled()) {
                    me.showContext();
                }
                break;

            case KEYUP:
                if (me.isHintsDivDisplayed()) {
                    if (me.highlighted > 0) {
                        me.highlighted--;
                    }
                    me.changeHighlight(true);
                    return me.cancelEvent(ev);
                }
                break;

            case KEYDOWN:
                if (me.isHintsDivDisplayed()) {
                    if (me.highlighted < (me.hints.length - 1)) {
                        me.highlighted++;
                    }
                    me.changeHighlight(true);
                    return me.cancelEvent(ev);
                }
                break;

            case PGUP:
                if (me.isHintsDivDisplayed()) {
                    me.highlighted = Math.max(0, me.highlighted - 9);
                    me.changeHighlight(true);
                    return me.cancelEvent(ev);
                }
                break;

            case PGDOWN:
                if (me.isHintsDivDisplayed()) {
                    me.highlighted = Math.min(me.hints.length - 1, me.highlighted + 9);
                    me.changeHighlight(true);
                    return me.cancelEvent(ev);
                }
                break;

            case SPACE:
                // Force show hint
                if (this.autoCompleteEnabled()) {
                    if (ev.ctrlKey) {
                        me.autoComplete = true;
                        me.showHints(null);
                        return me.cancelEvent(ev);
                    }
                }
                break;

            case BACKSPACE:
                if (me.isHintsDivDisplayed()) {
                    // We need to find out if we deleted the dot, if so, IntelliSense should be hidden
                    var pos = this.currentLinePos();
                    if (pos > 0) {
                        var charToDelete = this.currentLineText()[pos - 1];
                        if (charToDelete == '.') {
                            me.hideHints(false);
                        }
                    }
                }
                break;
        }

        if (key == ESC) {
            return me.cancelEvent(ev);
        }
    };

    this.handleKeyUp = function(ev) {
        var keyCode = me.getKeyCode(ev);
        if (this.autoCompleteEnabled()) {
            // Use hint on ENTER or TAB when hints are displayed (for Chrome & IE - they do not handle it on KeyPress)
            this.handleEnterTab(keyCode, ev);

            switch (keyCode) {
                case BACKSPACE:
                case DELETE:
                    me.showContext(null);
                    me.findHint(null, true);
                    break;
            }
        }
    };

    this.handleKeyPress = function(ev) {
        if (this.autoCompleteEnabled()) {
            var key = me.getCharCode(ev);
            var keyCode = me.getKeyCode(ev);

            var character = String.fromCharCode(key);

            // Use hint on ENTER or TAB when hints are displayed
            this.handleEnterTab(keyCode, ev);

            // For some keys use hint should be triggered
            switch (key) {

                case SPACE:
                    if (!ev.ctrlKey) {
                        if (me.isHintsDivDisplayed()) {
                            me.useHint();
                            //return me.cancelEvent(ev);
                        }
                    }
                    break;

                case SEMICOLON:
                case DOT:
                case COMMA:
                case LEFTBRACKET:
                case RIGHTBRACKET:
                case LEFTINDEXER:
                    me.useHint();
                    break;
            }

            switch (key) {

                case 0:
                    // Special control keys - do nothing
                    return;

                case RIGHTBRACKET:
                case RIGHTINDEXER:
                case COMMA:
                case LEFTINDEXER:
                case LEFTBRACKET:
                    me.hideHints(true);
                    me.showContext(character);
                    break;

                case SEMICOLON:
                    me.hideHints(true);
                    break;

                case PERCENT:
                case RIGHTCURLY:
                case DQUOTE:
                    me.hideHints(false);
                    break;

                case DOT:
                    me.showHints(character);
                    break;

                default:
                    // If we have alphanumeric char we might need to show the hints
                    if (me.showHintNextTime && !me.isHintsDivDisplayed() && character.match(ALPHANUMERIC_REGEX)) {
                        me.showHints(character);
                        me.showHintNextTime = false;
                    }
                    me.findHint(character, false);
                    break;
            }
        } else {
            this.hideHints();
            this.hideQuickContext();
            this.hideContext();
        }
    };

    this.handleEnterTab = function(keyCode, ev) {
        switch (keyCode) {
            case ENTER:
                if (this.isHintsDivDisplayed()) {
                    this.useHint();
                    return this.cancelEvent(ev);
                }
                break;

            case TAB:
                if (this.isHintsDivDisplayed()) {
                    this.useHint(true);
                    return this.cancelEvent(ev);
                }
                break;
        }
    };

    this.handleMouseUp = function(ev) {
        this.hideHints();
        this.hideQuickContext();
        this.hideContext();
    };

    // Hides all the divs
    this.hideAutoCompletion = function() {
        this.hideHints(true);
        this.hideContext();
        this.hideQuickContext();
    };

    /*
    * Method quick context help methods.
    */

    // Hides the method context.
    this.hideQuickContext = function() {
        this.hideQuickContextDiv();
    };

    // Shows the hints div.
    this.showQuickContextDiv = function() {
        this.quickContextDiv.style.display = 'block';
    };

    // Hides the hints div.
    this.hideQuickContextDiv = function() {
        this.quickContextDiv.style.display = 'none';
    };

    // Creates context div.
    this.createQuickContextDiv = function(help) {

        this.quickContextDiv.innerHTML = help;
        this.quickContextDiv.className = "AutoCompleteContext";
        this.quickContextDiv.style.position = 'absolute';

    };

    // Displays quick context help.
    this.showQuickContext = function() {
        var help = methodComments[this.hints[this.highlighted]];
        if (help) {
            this.createQuickContextDiv(help);
            this.showQuickContextDiv();
        } else {
            this.hideQuickContext();
        }
        this.positionDivs();
    };


    /*
    * Method context help methods.
    */

    // Method called from callback return function to fill and show the current context.
    this.fillContext = function(value) {
        this.context = value;

        if ((this.context == null) || (this.context == '')) {
            this.hideContext();
        } else {
            this.createContextDiv();
            this.showContextDiv();
        }
    }

    // Shows the mehotd context.
    this.showContext = function(charCode) {
        if (!this.isInsideComment()) {
            var currentLineMacro;
            if (this.isMixedMode) {
                // If the mode is mixed, parse only macro text and return only relevant parts of the code
                currentLineMacro = this.getCurrentLineMacro();
            } else {
            currentLineMacro = this.currentLineText() + '\n\n' + this.currentLinePos();
            }
            if (charCode == null) {
                this.callbackArgument = currentLineMacro + '\ncontext\n';
            } else {
            this.callbackArgument = currentLineMacro.replace('\n\n', '\n' + charCode + '\n') + '\ncontext\n';
            }
            this.callbackContext();
        }
    }

    // Hides the method context.
    this.hideContext = function() {
        this.hideContextDiv();
    }

    // Shows the hints div.
    this.showContextDiv = function() {
        this.contextDiv.style.display = 'block';
        this.positionDivs();
    };

    // Hides the hints div.
    this.hideContextDiv = function() {
        this.contextDiv.style.display = 'none';
    };

    // Creates context div.
    this.createContextDiv = function() {

        this.contextDiv.innerHTML = this.context;
        this.contextDiv.className = "AutoCompleteContext";
        this.contextDiv.style.position = 'absolute';

    };

    /*
    * Hints methods.
    */

    // Highlights the first hint with the prefix of currently typed identifier
    this.findHint = function(lastChar, hideIfEmpty) {
        // Hide the hints if the line is empty, or we are at the end of the command
        if (hideIfEmpty) {
            var currentText = this.currentLineText();
            if ((currentText == '') || currentText.charAt(this.currentLinePos() - 1) == ';') {
                this.hideHints(true);
                return;
            }
        }

        var identifier = this.locateIdentifier()[0];
        if (lastChar) {
            identifier += lastChar;
        }

        if (identifier != '') {
            identifier = identifier.toLowerCase();
            // Find the first item with given prefix
            var onlyMatch = true;
            var match = -1;
            for (i in this.hints) {
                if (this.hints[i].toLowerCase().indexOf(identifier) == 0) {
                    if (match != -1) {
                        onlyMatch = false;
                    } else {
                        match = i;
                    }
                }
            }

            // Highlight matched hint
            this.highlighted = match;
            this.changeHighlight(true);

            return onlyMatch;
        } else {
            // If identifier is empty, highlight first item
            if (this.hints.length > 0) {
                this.highlighted = 0;
                this.changeHighlight(true);
            }
        }

        return false;
    }

    // Method called from callback return function to fill and show the current hints.
    this.fillHints = function(value) {
        if (value != '') {
            this.hints = value.split('$');
        } else {
            this.hints = new Array();

            // If we did not get any result there is no point showing hint when alphanumerical key is pressed
            this.showHintNextTime = false;
        }

        if (this.autoComplete) {
            this.autoComplete = false;
            if (this.findHint(null, false)) {
                // If only single or no hint found, use it
                this.useHint();
            } else {
                this.createHintsDiv();
                this.showHintsDiv();
                this.findHint(null, false);
            }
        } else if (this.hints.length > 0) {
            this.createHintsDiv();
            this.showHintsDiv();
            this.findHint(null, true);
        } else {
            this.hideHints();
        }
    }

    // Gets the current line number
    this.getLineNumber = function() {
        if (this.elem.lineNumber) {
            return this.elem.lineNumber(this.elem.cursorPosition().line);
        }
        else {
            return this.elem.getCursor().line;
        }
    }

    this.getLineContent = function(i) {
        if (this.elem.nthLine) {
            return this.elem.lineContent(this.elem.nthLine(i));
        }
        else {
            return this.elem.getLine(i);
        }
    }

    // Calls the callback function - ensures to show the hints.
    this.showHints = function(charCode) {
        if (!this.isInsideComment()) {
            if (!this.isMixedMode) {
                // If the mode is not mixed, pass current line and all the text before as parameters
                var prevText = '';
                var actLine = this.getLineNumber();
                for (i = 1; i < actLine; i++) {
                    prevText += this.getLineContent();
                }

                if (charCode == null) {
                    this.callbackArgument = this.currentLineText() + '\n\n' + this.currentLinePos() + '\nhint\n' + prevText;
                } else {
                    this.callbackArgument = this.currentLineText() + '\n' + charCode + '\n' + this.currentLinePos() + '\nhint\n' + prevText;
                }
            } else {
                // If the mode is mixed, parse only macro text and return only relevant parts of the code

                // Locate part of current line which is macro text (might be whole line)
                var currentLineMacro = this.getCurrentLineMacro();

                // Parse all the macro parts before actual line
                var prevMacros = '';
                var lastPos = 0;
                var prevText = this.getTextToCaret(false);
                var brackets = 0;
                for (i = 0; i < prevText.length - 2; i++) {
                    // If we are in mixed mode stop before we run to {%
                    if ((prevText.charAt(i) == '{') && (prevText.charAt(i + 1) == '%')) {
                        if (brackets == 0) {
                            lastPos = i + 2;
                        }
                        brackets++;
                    }
                    if ((prevText.charAt(i) == '%') && (prevText.charAt(i + 1) == '}')) {
                        brackets--;
                        // Append inside of a macro environment
                        if (brackets == 0) {
                            prevMacros += ";" + prevText.substring(lastPos, i);
                        }
                    }
                }

                // Append end of text if the environment is not finished
                if (brackets > 0) {
                    prevMacros += ";" + prevText.substring(lastPos, prevText.length - 1);
                }

                if (charCode == null) {
                    this.callbackArgument = currentLineMacro + '\nhint\n' + prevMacros;
                } else {
                    this.callbackArgument = currentLineMacro.replace('\n\n', '\n' + charCode + '\n') + '\nhint\n' + prevMacros;
                }
            }

            // Do the callback and fill the hints collection
            this.callbackHint();
        }
    }

    // Hide the hints.
    this.hideHints = function(nextTimeShow) {
        if (nextTimeShow != null) {
            this.showHintNextTime = nextTimeShow;
        }
        this.hideHintsDiv();
        this.highlighted = -1;
        this.hints = new Array();
        this.hideQuickContext();

        // When hints are not displayed ENTER and TAB should be handled normally, by editor
        this.elem.doNotHandleKeys = false;
    }

    this.replaceRange = function(newText, line, from, to) {
        if (this.elem.replaceRange) {
            var fromP = { line: line, ch: from };
            var toP = { line: line, ch: to };

            this.elem.replaceRange(newText, fromP, toP);
        }
        else {
            this.elem.selectLines(line, from, line, to);
            this.elem.replaceSelection(newText);
        }
    }

    // Uses the selected hint to code.
    this.useHint = function(isTabUsage) {
        if ((this.hints.length > 0) && (this.highlighted > -1)) {

            var cursorIndex = -1;
            var hint = this.hints[this.highlighted].split('\n');
            var hintToUse = hint[0];
            var offset = 0;
            if (hintToUse) {
                // Use code snippet when the tab was used to fire the hint usage
                if (isTabUsage && (hint.length > 1) && (hint[1] != '')) {
                    cursorIndex = hint[1].indexOf('|');
                    hintToUse = hint[1].replace('|', '');
                }
                else if (hint.length > 1) {
                    //hintToUse += "()";
                    //offset = 1;
                }
            }

            // Locate the current identifier
            var currentIdentifier = this.locateIdentifier();
            var pos1 = currentIdentifier[1];
            var pos2 = currentIdentifier[2];

            var currCursorLine = this.elem.cursorLine();
            if (hintToUse[0] == '[') {
                pos1 = pos1 - 1;
                this.replaceRange(hintToUse, currCursorLine, pos1, pos2);
            } else {
                this.replaceRange(hintToUse, currCursorLine, pos1, pos2);
            }

            var pos;
            if (cursorIndex == -1) {
                pos = { line: currCursorLine, ch: pos1 + hintToUse.length - offset };

            } else {
                pos = { line: currCursorLine, ch: pos1 + cursorIndex - offset };
            }
            this.elem.setCursor(pos);
            this.elem.focus();
        }
        this.hideHints(true);
    };

    // Shows the hints div.
    this.showHintsDiv = function() {
        this.hintsDiv.style.display = 'block';
        if (this.hints.length > 0) {
            this.highlighted = 0;
            this.changeHighlight(true);

            // When hints are displayed ENTER and TAB keys should not be handled by editor
            this.elem.doNotHandleKeys = true;
        }
        this.positionDivs();
    };

    // Hides the hints div.
    this.hideHintsDiv = function() {
        this.hintsDiv.style.display = 'none';
    };

    // Hides the hints div.
    this.isHintsDivDisplayed = function() {
        return this.hintsDiv.style.display == 'block';
    };

    // Changes the highlighted item in the hints div
    this.changeHighlight = function(adjustScrollBar) {
        var lis = this.hintsDiv.getElementsByTagName('LI');
        for (i in lis) {
            var li = lis[i];
            if (li != null) {
                if (this.highlighted == i) {
                    li.className = "selected";
                }
                else {
                    li.className = "";
                }
            }
        }
        this.showQuickContext();
        if (adjustScrollBar && this.highlighted != -1) {
            this.hintsDiv.scrollTop = 20 * this.highlighted;
        }
    };

    // Creates the hints div.
    this.createHintsDiv = function() {
        var ul = document.createElement('ul');

        // Create an array of LI's for the words.
        for (i in this.hints) {
            var hint = this.hints[i];
            if (hint == '----') {
                var li = document.createElement('li');
                var sep = document.createElement('hr');
                li.appendChild(sep);
                ul.appendChild(li);
            } else {
                var index = hint.indexOf('\n');
                var isMethod = (index >= 0);
                var isSnippet = (index < hint.length - 1);
                var word = (isMethod ? hint.substring(0, index) : hint);

                var li = document.createElement('li');
                var img = document.createElement('img');
                var a = document.createElement('a');

                img.src = (isMethod ? (isSnippet ? SNIPPET_IMG : METHOD_IMG) : PROPERTY_IMG);
                img.alt = (isMethod ? 'm' : 'p');

                a.href = "javascript:";
                a.innerHTML = word;

                li.appendChild(img);
                li.appendChild(document.createTextNode(" "));
                li.appendChild(a);

                if (me.highlighted == i) {
                    li.className = "selected" + (isMethod ? " method" : " property");
                }

                ul.appendChild(li);
            }
        }

        this.hintsDiv.replaceChild(ul, this.hintsDiv.childNodes[0]);

        ul.onmouseover = function(ev) {
            //Walk up from target until you find the LI.
            var target = me.getEventSource(ev);
            while (target.parentNode && target.tagName.toLowerCase() != 'li') {
                target = target.parentNode;
            }

            var lis = me.hintsDiv.getElementsByTagName('LI');

            for (i in lis) {
                var li = lis[i];
                if (li == target) {
                    me.highlighted = i;
                    break;
                }
            }
            me.changeHighlight(false);
        };

        ul.onmouseup = function(ev) {
            me.useHint();
            me.hideHints();
            me.cancelEvent(ev);
            return false;
        };

        this.hintsDiv.className = "AutoCompleteHints";
        this.hintsDiv.style.position = 'absolute';

    };


    /*
    * Helper functions ensuring cross-browser functionality.
    */

    // Determines whether the cursor is inside a comment
    this.isInsideComment = function() {
        var prevText = this.getTextToCaret(true, true);
        var isLineComment = true;
        var isMultilineComment = true;
        for (i = prevText.length - 1; i > 0; i--) {
            if ((prevText.charAt(i) == '\n') && isLineComment) {
                // If we run into new line first it cannot be inside an inline comment
                isLineComment = false;
            } else if ((prevText.charAt(i) == '/') && (prevText.charAt(i - 1) == '/') && isLineComment) {
                // If we run into // first it is inside an inline comment
                return true;
            } else if ((prevText.charAt(i) == '/') && (prevText.charAt(i - 1) == '*') && isMultilineComment) {
                // If we run into */ first it is not inside a comment
                isMultilineComment = false;
            } else if ((prevText.charAt(i) == '*') && (prevText.charAt(i - 1) == '/') && isMultilineComment) {
                // If we run into /* first it is inside a comment
                return true;
            }
        }
        return false;
    }

    // Sets the correct position to a hints div.
    this.positionDivs = function() {
        var frame = null;

        if (this.elem.getWrapperElement) {
            frame = this.elem.getWrapperElement();
        }
        else {
            frame = this.elem.frame;
        }

        // Get the maximum coordinates (autocomplete should not be outside the editor)
        var elemPos = this.getElementPosition(frame);
        var maxHintsX = elemPos.x + frame.offsetWidth - this.hintsDiv.offsetWidth - 20;

        // Actual caret coordinates
        var caretPos = this.elem.cursorCoords();
        var x = Math.min(caretPos.x - 5, maxHintsX);
        var y = caretPos.y;

        // Indicates whether to show quick context help on the left side
        var quickContextLeft = x <= elemPos.x + frame.offsetWidth / 2;
        var hintDown = y <= elemPos.y + frame.offsetHeight / 2;

        // Make sure force above settings is applied
        if (this.forceAbove) {
            hintDown = false;
        }

        // Adjust by offset
        if (this.leftOffset) {
            x -= this.leftOffset;
        }
        if (this.topOffset) {
            y -= this.topOffset;
        }

        if (hintDown) {
            y += 25;

            this.contextDiv.style.left = x + 'px';
            this.contextDiv.style.top = y + 'px';

            if (this.contextDiv.style.display != 'none') {
                y += this.contextDiv.offsetHeight + 5;
            }
        } else {
            y -= 5;

            if (this.contextDiv.style.display != 'none') {
                y -= this.contextDiv.offsetHeight;

                this.contextDiv.style.left = x + 'px';
                this.contextDiv.style.top = y + 'px';

                y -= 5;
            }

            y -= this.hintsDiv.offsetHeight;
        }

        this.hintsDiv.style.left = x + 'px';
        this.hintsDiv.style.top = y + 'px';

        if (this.hintsDiv.style.display != 'none') {
            if (quickContextLeft) {
                x += this.hintsDiv.offsetWidth + 5;
            } else {
                x -= this.quickContextDiv.offsetWidth + 5;
            }
        }

        this.quickContextDiv.style.left = x + 'px';
        this.quickContextDiv.style.top = y + 'px';
    };

    this.locateIdentifier = function(onlyPrefix) {
        var text = this.currentLineText();
        var pos1 = 0;

        for (i = this.currentLinePos() - 1; i >= 0; i--) {
            var character = text.charAt(i);
            if ((character != '') && !character.match(ALPHANUMERIC_REGEX)) {
                pos1 = i + 1;
                break;
            }
        }
        var pos2;
        if (onlyPrefix) {
            pos2 = this.currentLinePos();
        } else {
            pos2 = text.length;
            for (i = this.currentLinePos(); i < text.length; i++) {
                var character = text.charAt(i);
                if ((character != '') && !character.match(ALPHANUMERIC_REGEX)) {
                    pos2 = i;
                    break;
                }
            }
        }
        return new Array(text.substring(pos1, pos2), pos1, pos2);
    };

    this.currentLinePos = function() {
        if (this.elem.cursorPosition) {
            return this.elem.cursorPosition().character;
        }
        else {
            return this.elem.getCursor().ch;
        }
    };

    this.currentLineText = function() {
        return this.elem.lineContent(this.elem.cursorLine());
    };

    this.autoCompleteEnabled = function() {
        // If it's not mixed mode, auto completion is always enabled
        if (!this.isMixedMode) {
            return true;
        } else {
            // In mixed mode, only if the caret is inside macro environment the auto completion is enabled
            // Get the whole text before actual line
            var text = this.getTextToCaret(true);
            for (i = text.length; i > 0; i--) {
                var char1 = text.charAt(i);
                var char2 = text.charAt(i - 1);
                if ((char2 == '{') && (char1 == '%')) {
                    return true;
                } else if ((char2 == '%') && (char1 == '}')) {
                    return false;
                }
            }
            return false;
        }
    }

    // Returns whole text up to caret position (if includeCurrentLine is false, than it returns all the text from lines before current line).
    // If includeNewLineChars than \n is added inbetween lines
    this.getTextToCaret = function(includeCurrentLine, includeNewLineChars) {
        var text = '';
        var newLine = '';
        if (includeNewLineChars) {
            newLine = '\n';
        }
        var currLine = this.getLineNumber(); ;
        for (i = 0; i < currLine; i++) {
            text += newLine + this.getLineContent(i);
        }
        if (includeCurrentLine) {
            text += newLine + this.currentLineText().substring(0, this.currentLinePos() + 1);
        }
        return text;
    }

    // Returns macro part around cursor of current line
    this.getCurrentLineMacro = function() {
        var caretPos = this.currentLinePos();
        var actLineText = this.currentLineText();
        var currentLineMacro = '';
        var pos1 = 0;
        var pos2 = actLineText.length;
        for (i = this.currentLinePos(); i > 0; i--) {
            // Stop when we run into {%
            if ((actLineText.charAt(i - 1) == '{') && (actLineText.charAt(i) == '%')) {
                pos1 = i + 1;
                caretPos -= i + 1;
                break;
            }
        }
        for (i = this.currentLinePos(); i < actLineText.length - 1; i++) {
            // Stop when we run into %}
            if ((actLineText.charAt(i) == '%') && (actLineText.charAt(i + 1) == '}')) {
                pos2 = i;
                break;
            }
        }
        currentLineMacro = actLineText.substring(pos1, pos2);
        return currentLineMacro + '\n\n' + caretPos;
    }

    this.getCharCode = function(ev) {
        if (ev) {
            isIE = (window.ActiveXObject) ? true : false;
            if (isIE) {
                return ev.keyCode;
            } else {
                return ev.charCode;
            }
        }
        if (window.event) {
            return window.event.keyCode;
        }
    };

    this.getKeyCode = function(ev) {
        if (ev) {
            return ev.keyCode;
        }
        if (window.event) {
            return window.event.keyCode;
        }
    };

    this.getEventSource = function(ev) {
        var e = window.event || ev;
        return e.srcElement || e.target;
    };

    this.cancelEvent = function(ev) {
        if (ev) {
            ev.stopPropagation();
            ev.preventDefault();
        }
        if (window.event) {
            window.event.returnValue = false;
        }
        return false;
    };

    this.getElementPosition = function(e) {
        var position = { x: 0, y: 0 };
        while (e) {
            position.x += e.offsetLeft;
            position.y += e.offsetTop;
            e = e.offsetParent;
        }
        return position;
    };
}