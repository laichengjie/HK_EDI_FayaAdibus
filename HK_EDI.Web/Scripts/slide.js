$(function () {
    var sWidth = $(".slidecontainer").width();
    $(".slidecontainer").css("height", (sWidth * 481) / 1308 + "px");
    var len = $(".slidecontainer ul li").length;
    var index = 0;
    var loopstyle = 0;
    var picTimer;

    var btn = "<div class='btnBg'></div><div class='btn'>";
    for (var i = 0; i < len; i++) {
        btn += "<span></span>";
    }
    btn += "</div>"
    $(".slidecontainer").append(btn);
    $(".slidecontainer .btnBg").css("opacity", 0.3);


    $(".slidecontainer .btn span").mouseenter(function () {
        index = $(".slidecontainer .btn span").index(this);
        console.log(index);
        showPics(index);
    }).eq(0).trigger("mouseenter");

    $(".slidecontainer ul").css("width", sWidth * (len + 1));
    $(".slidecontainer ul li").css("width", sWidth);
    $(".slidecontainer ul li").css("height", (sWidth * 481) / 1308 + "px");

    $(".slidecontainer").hover(function () {
        clearInterval(picTimer);
    }, function () {
        picTimer = setInterval(function () {
            if (index == len) {
                showFirPic();
                index = 0;
            } else {
                showPics(index);
            }
            index++;
        }, 5000);
    }).trigger("mouseleave");

    function showPics(index) {
        var nowLeft = -index * sWidth;
        $(".slidecontainer ul").stop(true, false).animate({ "left": nowLeft }, 500);
        $(".slidecontainer .btn span").removeClass("on").eq(index).addClass("on")
    }

    function showFirPic() {
        $(".slidecontainer ul").append($(".slidecontainer ul li:first").clone());
        var nowLeft = -len * sWidth;
        $(".slidecontainer ul").stop(true, false).animate({ "left": nowLeft }, 500, function () {
            $(".slidecontainer ul").css("left", "0");
            $(".slidecontainer ul li:last").remove();
        });
        $(".slidecontainer .btn span").removeClass("on").eq(0).addClass("on");
    }
});