//$(function() {
    
    //var dataRemap = [];
    //var columnHeight = $('ul.tiles li:first-child').height();
    
    //var totalHeight = $('div.tiles').height();
    
    //var rows = Math.floor(totalHeight / columnHeight);
    
    //$( "li" ).each(function( index ) {
    //    dataRemap.push($(this).text());
    //});
    
    //$('ul.tiles').remove();

    //var ul;
    //$.each(dataRemap, function (index, value) {
    //    if(index % rows === 0) {
    //        $('div.tileExpander').append(ul);
    //        ul = $('<ul>').addClass("tiles");
    //    }
    //    var li = $('<li>').addClass("blue").addClass("big").append(value);
    //    ul.append(li);
    //});
    //$('div.tileExpander').append(ul);
//});

//(function ($) {
//    $.fn.tiles = function (options) {
//        console.log(options);

//        var that = this;
//        var totalHeight = that.height();
//        var columnHeight = $("li:first-child", that).height();
//        var items = $("li", that);
//        var rows = Math.floor(totalHeight / columnHeight);

//        $(that).html = '';

//        var ul;
//        $.each(items, function (index, value) {
//            if (index % rows === 0) {
//                ul = $('<ul>').addClass("tiles");
//                that.append(ul);
//            }
//            ul.append(this);
//        });
//        that.append(ul);

//        return this;
//    }
//})(jQuery);

(function ($) {
    $.fn.tiles = function (options) {
        
        var that = this;
        var totalHeight = that.height();
        var columnHeight = 200;
        var rows = Math.floor(totalHeight / columnHeight);

        $(that).html = '';

        var ul;
        $.each(options, function (index, tile) {

			//create new colomn if height is reached
        	if (index % rows === 0) {
            	ul = $('<ul>').addClass("tiles");
            	that.append(ul);
            }

        	//create tile
            var htmlTile = $('<li>').addClass("fdd big");
           
        	//add link to tile
            var tileLink = $('<a>')
						.attr('href', tile.Url)
						.append($('<h1>')
							.text(tile.Title)
						);	
            htmlTile.append(tileLink);
            
        	//create preview items
            if (tile.PreviewItems != null) {
            	
            	$.each(tile.PreviewItems, function (index, previewItem) {
            		tileLink.append($('<h2>')
						.text(previewItem)
					);            		
            	});
            }
            console.log(tileLink);

            if (tile.Copyright != null) {
            	tileLink.append(
					$('<span>')
						.text(tile.Copyright)
				);
            }

            ul.append(htmlTile);
        });
        that.append(ul);

        return this;
    }
})(jQuery);