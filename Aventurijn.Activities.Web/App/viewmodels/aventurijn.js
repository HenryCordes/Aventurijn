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

ko.bindingHandlers.datepicker = {
    init: function(element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $(element).datepicker("destroy");
        });

    },
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        //handle date data coming via json from Microsoft
        if (String(value).indexOf('/Date(') == 0) {
            value = new Date(parseInt(value.replace(/\/Date\((.*?)\)\//gi, "$1")));
        }
        else if (String(value).indexOf('-') > 0){
            
           var dateArray = String(value).split('-');
            if (dateArray.length === 3) {
                value = new Date(parseInt(dateArray[2]),
                    parseInt(dateArray[1]),
                    parseInt(dateArray[0]));
            }
        }

        current = $(element).datepicker("getDate");

        if (value - current !== 0) {
             $(element).datepicker("setDate", value);
        }
    }
};


//ko.dirtyFlag = function(root, isInitiallyDirty) {
//    var result = function() {},
//        _initialState = ko.observable(ko.toJSON(root)),
//        _isInitiallyDirty = ko.observable(isInitiallyDirty);

//    result.isDirty = ko.computed(function() {
//        return _isInitiallyDirty() || _initialState() !== ko.toJSON(root);
//    });

//    result.reset = function() {
//        _initialState(ko.toJSON(root));
//        _isInitiallyDirty(false);
//    };

//    return result;
//};
