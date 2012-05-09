(function($) {
    $.widget("ui.combobox", {
        _create: function() {
            var self = this,
				select = this.element.hide(),
				selected = select.children(":selected"),
				value = selected.val() ? selected.text() : "";
            var input = this.input = $('#' + select.attr('id').replace('dropDownList', 'txtCombo'))
					.autocomplete({
                        appendTo: select.parent(),
					    delay: 0,
					    minLength: 0,
					    source: function(request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function() {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function(event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function(event, ui) {
					        if (!ui.item) {
					            var jThis = $(this),
					                matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(jThis.val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function() {
					                if (jThis.text().match(matcher)) {
					                    this.selected = valid = true;
					                    return false;
					                }
					            });
					            if (!valid) {
					                return false;
					            }
					        }
					    }
					})
					.removeClass("ui-autocomplete-input ui-widget ui-widget-content")
					.addClass("TextBoxField")

			input.data("autocomplete")._renderItem = function(ul, item) {
                ul.addClass("ComboSelector");
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a class=\"ComboSelect\">" + item.label + "</a>")
						.appendTo(ul);
            };

            this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.insertAfter(input)
					.removeClass("ui-corner-all ui-button ui-widget ui-state-default ui-button-text-only")
					.click(function() {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("close");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
        },

        destroy: function() {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);
