var NewDetailsTool = {};
//商品分类显示和隐藏
NewDetailsTool.ProductTypeShowAndHide = function (currentobj, typecontainerid, btnshowandhideid) {
    if ($(currentobj).hasClass("switch_show")) {
        $("#" + btnshowandhideid).html("<span class=\"icon_more\"></span>展开</a>");
        $("#" + typecontainerid).css("display", "none");
        $(currentobj).removeClass("switch_show");
    } else {
        $("#" + btnshowandhideid).html("<span class=\"icon_more\"></span>收起</a>");
        $("#" + typecontainerid).css("display", "block");
        $(currentobj).addClass("switch_show");
    }
};
//商品子分类的显示和隐藏
NewDetailsTool.ChildTypeShowAndHide = function (currentobj, childtypeid) {
    var isshow = $(currentobj).attr("isshow");
    if (isshow == "false") {
        $("#" + childtypeid + " [isshow='false']").each(function () {
            $(this).attr("isshow", "true");
            $(this).css("display", "block");
        });
        $(currentobj).attr("isshow", "true");
        $(currentobj).text("收起");
    } else {
        $("#" + childtypeid + " [isshow='true']").each(function () {
            $(this).attr("isshow", "false");
            $(this).css("display", "none");
        });
        $(currentobj).attr("isshow", "false");
        $(currentobj).text("展开");
    }
};
//获取用户评论
NewDetailsTool.GetUserComment = function (productcode, index) {
    $.ajax({
        type: "GET",
        async: false,
        url: "http://tuan.tiantian.com/sale/GetUserCommentPaged/" + productcode + "/" + index,
        success: function (data) {
            var jsondata = eval("(" + data + ")");
            NewDetailsTool.ShowUserComment(jsondata.CommentList);
            $("#commentnavigation").html(unescape(jsondata.Navigation));
        }
    });
};
//显示用户评论
NewDetailsTool.ShowUserComment = function (jsondata) {
    var html = [];
    if (jsondata.length > 0) {
        for (var i = 0; i < jsondata.length; i++) {
            var item = jsondata[i];
            html.push("<div class=\"kb_show\">");
            html.push("<div class=\"kb_showl\">");
            html.push("<img width=\"45\" height=\"45\" src=\"", unescape(item.UserUserLogoID), "\">");
            html.push("</div>");
            html.push("<div class=\"kb_showr\">");
            html.push("<span class=\"show_sj\"></span>");
            html.push("<div class=\"show_con\">");
            html.push("<div class=\"show_con_t\">");
            html.push("<div class=\"t_r\">");
            html.push("<span class=\"r1\">评分：</span><span class=\"r2\" style=\"width:", item.StarPercent, "px;\"></span></div>");
            html.push("<span class=\"t_l1\"><b>会员名：</b>", unescape(item.UserID), "</span> ");
            html.push("<span class=\"t_l2\"><b>会员等级：</b>", unescape(item.UserTypeName), "</span>");
            html.push("</div>");
            html.push("<div class=\"show_con_c\">");
            html.push("<h3>", unescape(item.CommentTitle), "</h3>");
            html.push("<p>", unescape(item.CommentContent), "</p>");
            html.push("<p class=\"date\">评论日期：", item.CommentCreateDate, "</p>");
            html.push("</div>");
            html.push("</div>");
            html.push("</div>");
            html.push("<div class=\"clear\"></div></div>");
        }
        $("#divusercomment").html(html.join(""));
    }
};
//添加到购物车
NewDetailsTool.AddToShoppingTrolley = function (url) {
    location.href = url;
};


//倒计时对象
function TimerInterval() {
    //倒计时格式
    var formatHtml = { "Details": "D天H时M分S秒R" };
    //定时器
    function startTimer(timerlistobj, moudleId) {
        $(timerlistobj).each(function () {
            var contentId = $(this).attr("id");
            var totalseconds = parseInt($("#hid_" + moudleId + "_" + contentId).val());
            var ms = {
                D: Math.floor(totalseconds / (60 * 60 * 24)),
                H: Math.floor(totalseconds / (60 * 60)) % 24,
                M: Math.floor(totalseconds / (60)) % 60,
                S: Math.floor(totalseconds) % 60,
                R: 0
            };

            $(this).html(formatHtml[moudleId].replace(/D|H|M|S|R/g, function (a) {
                if (parseInt(ms[a]) <= 9) {
                    if (a == "R") {
                        return $("#hidtimer").val();
                    }
                    return ms[a];
                }
                else {
                    return ms[a];
                }
            }));
            if ($("#hidtimer").val() == "0") {
                $("#hidtimer").val("9");
                $("#hid_" + moudleId + "_" + contentId).val(totalseconds - 1);
            } else {
                $("#hidtimer").val(parseInt($("#hidtimer").val()) - 1);
            }
        });
    };
    //开启团购三级页的定时器
    this.StartDetailsTimer = function () {
        if (TimerInterval.TimerFlag.DetailsTimerFlag != "") {
            clearInterval(TimerInterval.TimerFlag.DetailsTimerFlag);
        }
        TimerInterval.TimerFlag.DetailsTimerFlag = setInterval(function () {
            startTimer("p[tag='Details']", "Details");
        }, 100);
    };
}

TimerInterval.TimerFlag = { "DetailsTimerFlag": "" };


$(function () {
    function scrollDoor() {
    }

    scrollDoor.prototype = {
        sd: function (menus, divs, openClass, closeClass) {
            var _this = this;
            if (menus.length != divs.length) {
                alert("菜单层数量和内容层数量不一样!");
                return false;
            }
            for (var i = 0; i < menus.length; i++) {
                _this.$(menus[i]).value = i;
                _this.$(menus[i]).onmouseover = function () {
                    for (var j = 0; j < menus.length; j++) {
                        _this.$(menus[j]).className = closeClass;
                        _this.$(menus[j]).childNodes[0].className = "";
                        _this.$(divs[j]).style.display = "none";
                    }
                    _this.$(menus[this.value]).className = openClass;
                    _this.$(menus[this.value]).childNodes[0].className = "ds11";
                    _this.$(divs[this.value]).style.display = "block";
                };
            }
        },
        $: function (oid) {
            if (typeof (oid) == "string")
                return document.getElementById(oid);
            return oid;
        }
    };
    //$(document).ready(function () {
    //  var SDmodel = new scrollDoor();
    //  SDmodel.sd(["ys1", "ys2", "ys3", "ys4", "ys5", "ys6", "ys7", "ys8", "ys9"], ["sy1", "sy2", "sy3", "sy4", "sy5", "sy6", "sy7", "sy8", "sy9"], "ds01", "ds02");
    //});

    //固定导航
    var height = 0;
    $(function () {
        var obj = document.getElementById("divnavigation");
        while (obj != null) {
            height += obj.offsetTop;
            obj = obj.offsetParent;
        }
    });
    $(window).scroll(function () {
        var scrollHeight = $(window).scrollTop();
        if (height > 0 && scrollHeight > height) {
            $("#divnavigation").addClass("tmc_tit2");
        } else {
            $("#divnavigation").removeClass("tmc_tit2");
        }
    });
    var $body = (window.opera) ? (document.compatMode == "CSS1Compat" ? $('html') : $('body')) : $('html,body');
    var scrollto = function () {
        var obj = $(this).attr('tab');
        if (!obj) {
            return true;
        }
        var v = $(obj).offset();
        if (v != null) {
            v = v.top - 60;
            $body.animate({
                scrollTop: v
            },
              10);
        }
    };
    $('#new_deal_tabs li').click(scrollto);

    //开启团购三级页的定时器
    if ($("p[tag='Details']").length > 0) {
        var detailsTimer = new TimerInterval();
        detailsTimer.StartDetailsTimer();
    }

    //最佳组合
    $("input[name='groupbuy']").click(function () {
        var sumvipprice = 0;
        var sumsaveprice = 0;
        var sumsaleprice = 0;
        var groupbuycount = 1;
        var firstsaleprice = parseFloat($("#firstgroupbuy").attr("saleprice"));
        var firstvipprice = parseFloat($("#firstgroupbuy").attr("vipprice"));


        $("input[name='groupbuy']:checked").each(function () {
            sumvipprice += parseFloat($(this).attr("vipprice"));
            sumsaleprice += parseFloat($(this).attr("saleprice"));
            groupbuycount += 1;
        });

        sumsaleprice += firstsaleprice;
        sumvipprice += firstvipprice;
        sumsaveprice = sumsaleprice - sumvipprice;
        $("#sumvipprice").html("总天天价：<span class=\"pink\">" + sumvipprice.toFixed(1) + "</span>元");
        $("#sumsaveprice").html("为您节省：<span>" + sumsaveprice.toFixed(1) + "</span>元");
        $("#sumsaleprice").html("总市场价：<span class=\"pink\">" + sumsaleprice.toFixed(1) + "</span>元");
        $("#groupbuycount").html(groupbuycount + "件");
    });

    $("#addgroupbuytoshopcar").click(function () {
        var productcode = "";
        var bigtypeid = null;
        var productInfo = "";
        var isExist = false;
        var isgroupbuy = null;
        var grouplistid = null;
        var domain = "http://shopping.tiantian.com/cart/addCart/";

        $("input[name='groupbuy']:checked").each(function () {
            productcode = $(this).attr("productcode");
            productInfo += ("," + productcode);
        });

        isgroupbuy = $("#firstgroupbuy").attr("isgroupbuy");
        productcode = $("#firstgroupbuy").attr("productcode");
        grouplistid = $("#firstgroupbuy").attr("grouplistid");

        if (isgroupbuy == "true") {
            productInfo = productcode + productInfo + "/" + grouplistid + "/1/1";
        }
        else {
            productInfo = productcode + productInfo + "/1";
        }
        location.href = domain + productInfo;
    });
    /* sidebar 滚动信息 */
    //var deal_sidebar_rebox = document.getElementById('deal_sidebar_rebox');
    //var item01 = document.getElementById('item_box01');
    //var item02 = document.getElementById('item_box02');

    //if (item01 == null || item02 == null) {
    //    return;
    //}
    //item02.innerHTML = item01.innerHTML;

    //var auto_c = function () {
    //    if (item02.offsetHeight - deal_sidebar_rebox.scrollTop <= 0) {
    //        deal_sidebar_rebox.scrollTop -= item01.offsetHeight;
    //    } else {
    //        $(deal_sidebar_rebox).animate({ 'scrollTop': '+=51' });
    //    }

    //};

    //var setAuto = setInterval(auto_c, 2000);
    //deal_sidebar_rebox.onmouseover = function () {
    //    clearInterval(setAuto);
    //};
    //deal_sidebar_rebox.onmouseout = function () {
    //    setAuto = setInterval(auto_c, 2000);
    //};
});