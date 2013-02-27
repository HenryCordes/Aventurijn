puntjes.message = (function () {

    var _init = function(element) {
        var $element = $(element),
            currentPosition = $element.css('position'),
            $message = $('<div class="message"><div></div></div>').hide();

        $element.append($message);

        //make sure that we can absolutely position against the original element
        if (currentPosition == 'auto' || currentPosition == 'static')
            $element.css('position', 'relative');

        //center 
        $message.css({
            position: 'absolute',
            top: '50%',
            left: '50%',
            'margin-left': -($message.width() / 2) + 'px',
            'margin-top': -($message.height() / 2) + 'px'
        });
    };

    var _show = function(message) {
        var $message = $('div.message');
        $message.find('div').text(message);
        $message.fadeIn('slow')
            .animate({ opacity: 1.0 }, 1400)
            .fadeOut('slow', function() {
                $message.text();
            });
    };

    return {
         init: _init,
         show: _show
    };
})();
