$(function () {
    $('.datefield').datepicker({ dateFormat: 'dd-mm-yy' });

    if (jQuery.validator) {
        
        jQuery.validator.addMethod(
            'date',
            function(value, element, params) {
                if (this.optional(element)) {
                    return true;
                }
                ;
                var result = false;
                try {
                    $.datepicker.parseDate('dd-mm-yy', value);
                    result = true;
                }
                catch(err) {
                    result = false;
                }
                return result;
            },
            ''
        );
    }
});


ko.bindingHandlers.loadingWhen = {
    init: function (element) {
        var 
            $element = $(element),
            currentPosition = $element.css('position'),
            $loader = $('<div class="loader"><div>Verwerken...<div></div>').hide();

        //add the loader
        $element.append($loader);
        
        //make sure that we can absolutely position the loader against the original element
        if (currentPosition == 'auto' || currentPosition == 'static')
            $element.css('position', 'relative');

        //center the loader
        $loader.css({
            position: 'absolute',
            top: '50%',
            left: '50%',
            'margin-left': -($loader.width() / 2) + 'px',
            'margin-top': -($loader.height() / 2) + 'px'
        });
    },
    update: function (element, valueAccessor) {
        var isLoading = ko.utils.unwrapObservable(valueAccessor()),
            $element = $(element),
            $childrenToHide = $element.children(':not(div.loader)'),
            $loader = $element.find('div.loader');

        if (isLoading) {
            $childrenToHide.css('visibility', 'hidden').attr('disabled', 'disabled');
            $loader.fadeIn('fast');
        }
        else {
            $loader.fadeOut('slow');
            $childrenToHide.css('visibility', 'visible').removeAttr('disabled');
        }
    }
};