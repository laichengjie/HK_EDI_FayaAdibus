/*
 * jqueryzoom Evolution 1.0.3 - Javascript Image magnifier
 *
 * Copyright (c) Engineer Renzi Marco(www.mind-projects.it)
 *
 * $Date: 12-12-2008
 *
 * ChangeLog: xqiang 2010-05-21 15:45
 *  
 */
(function($) {
	$.fn.jqueryzoom = function(options) {
		var settings = {
			xzoom: 200,
			//zoomed width default width
			yzoom: 200,
			//zoomed div default width
			offset: 10,
			//zoomed div default offset
			position: "right",
			//zoomed div default position,offset position is to the right of the image
			lens: 1,
			//zooming lens over the image,by default is 1;
			preload: 1
		};
		if (options) {
			$.extend(settings, options);
		}
		var noalt = '';
		$(this).hover(function() {
			var imageLeft = $(this).offset().left;
			var imageTop = $(this).offset().top;
			var imageWidth = $(this).children('img').get(0).offsetWidth;
			var imageHeight = $(this).children('img').get(0).offsetHeight;
			noalt = $(this).children("img").attr("alt");
			var bigimage = $(this).children("img").attr("longdesc");
			$(this).children("img").attr("alt", '');
			if ($("div.jqueryzoomdiv").get().length == 0) {
				$(this).after("<div class='jqueryzoomdiv'><img class='bigimg' src='" + bigimage + "'/></div>");
				$(this).append("<div class='jqueryzoomPup'>&nbsp;</div>");
			}
			if (settings.position == "right") {
				if (imageLeft + imageWidth + settings.offset + settings.xzoom > screen.width) {
					leftpos = imageLeft - settings.offset - settings.xzoom;
				} else {
					leftpos = imageLeft + imageWidth + settings.offset;
				}
			} else {
				leftpos = imageLeft - settings.xzoom - settings.offset;
				if (leftpos < 0) {
					leftpos = imageLeft + imageWidth + settings.offset;
				}
			}
			$("div.jqueryzoomdiv").css({
				top: imageTop,
				left: leftpos
			});
			$("div.jqueryzoomdiv").width(settings.xzoom);
			$("div.jqueryzoomdiv").height(settings.yzoom);
			$("div.jqueryzoomdiv").show();
			if (!settings.lens) {
				$(this).css('cursor', 'crosshair');
			}
			$(document.body).mousemove(function(e) {
				mouse = new MouseEvent(e);
				/*$("div.jqueryzoomPup").hide();*/
				var bigwidth = $(".bigimg").get(0).offsetWidth;
				var bigheight = $(".bigimg").get(0).offsetHeight;
				var scaley = 'x';
				var scalex = 'y';
				if (isNaN(scalex) | isNaN(scaley)) {
					var scalex = (bigwidth / imageWidth);
					var scaley = (bigheight / imageHeight);
					$("div.jqueryzoomPup").width((settings.xzoom) / (scalex * 1));
					$("div.jqueryzoomPup").height((settings.yzoom) / (scaley * 1));
					if (settings.lens) {
						$("div.jqueryzoomPup").css('visibility', 'visible');
					}
				}
				xpos = mouse.x - $("div.jqueryzoomPup").width() / 2 - imageLeft;
				ypos = mouse.y - $("div.jqueryzoomPup").height() / 2 - imageTop;
				if (settings.lens) {
					xpos = (mouse.x - $("div.jqueryzoomPup").width() / 2 < imageLeft) ? 0 : (mouse.x + $("div.jqueryzoomPup").width() / 2 > imageWidth + imageLeft) ? (imageWidth - $("div.jqueryzoomPup").width() - 2) : xpos;
					ypos = (mouse.y - $("div.jqueryzoomPup").height() / 2 < imageTop) ? 0 : (mouse.y + $("div.jqueryzoomPup").height() / 2 > imageHeight + imageTop) ? (imageHeight - $("div.jqueryzoomPup").height() - 2) : ypos;
				}
				if (settings.lens) {
					$("div.jqueryzoomPup").css({
						top: ypos,
						left: xpos
					});
				}
				scrolly = ypos;
				$("div.jqueryzoomdiv").get(0).scrollTop = scrolly * scaley;
				scrollx = xpos;
				$("div.jqueryzoomdiv").get(0).scrollLeft = (scrollx) * scalex;
			});
		},
		function() {
			$(this).children("img").attr("alt", noalt);
			$(document.body).unbind("mousemove");
			if (settings.lens) {
				$("div.jqueryzoomPup").remove();
			}
			$("div.jqueryzoomdiv").remove();
		});
		count = 0;
		if (settings.preload) {
			$('body').append("<div style='display:none;' class='jqPreload" + count + "'>mymyty</div>");
			$(this).each(function() {
				var imagetopreload = $(this).children("img").attr("longdesc");
				var content = jQuery('div.jqPreload' + count + '').html();
				jQuery('div.jqPreload' + count + '').html(content + '<img src=\"' + imagetopreload + '\">');
			});
		}
	}
})(jQuery);
function MouseEvent(e) {
	this.x = e.pageX;
	this.y = e.pageY;
}
