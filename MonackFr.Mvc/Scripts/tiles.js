$(function() {
    
    var dataRemap = [];
    var columnHeight = $('ul.tiles li:first-child').height();
    var totalHeight = $('div.tiles').height();

    var rows = Math.floor(totalHeight / columnHeight);
    
    $( "li" ).each(function( index ) {
        dataRemap.push($(this).text());
    });
    
    $('ul.tiles').remove();

    var ul;
    $.each(dataRemap, function (index, value) {
        if(index % rows === 0) {
            $('div.tileExpander').append(ul);
            ul = $('<ul>').addClass("tiles");
        }
        var li = $('<li>').addClass("blue").addClass("big").append(value);
        ul.append(li);
    });
    $('div.tileExpander').append(ul);
});

(function ($) {
    $.fn.tiles = function (options) {
        console.log(this);
        return this;
    }
})(jQuery);