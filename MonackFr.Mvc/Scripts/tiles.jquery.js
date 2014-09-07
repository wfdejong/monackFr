(function ($) {
	$.fn.tiles = function (options, onclick) {

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
						.attr('href', 'javascript:void(0)')
						.append($('<h1>')
							.text(tile.Title)
						);
            htmlTile.bind("click", function () { onclick(tile.Module) });
            htmlTile.append(tileLink);
            
        	//create preview items
            if (tile.PreviewItems != null) {
            	
            	$.each(tile.PreviewItems, function (index, previewItem) {
            		tileLink.append($('<h2>')
						.text(previewItem)
					);            		
            	});
            }
            
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