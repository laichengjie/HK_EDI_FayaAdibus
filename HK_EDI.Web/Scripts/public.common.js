/*
** nopCommerce custom js functions
*/



function OpenWindow(query, w, h, scroll) {
    var l = (screen.width - w) / 2;
    var t = (screen.height - h) / 2;

    winprops = 'resizable=0, height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + 'w';
    if (scroll) winprops += ',scrollbars=1';
    var f = window.open(query, "_blank", winprops);
}

function setLocation(url) {
    window.location.href = url;
}

function displayAjaxLoading(display) {
    if (display) {
        $('.ajax-loading-block-window').show();
    }
    else {
        $('.ajax-loading-block-window').hide('slow');
    }
}

function displayPopupNotification(message, messagetype, modal) {
    //types: success, error
    var container;
    if (messagetype == 'success') {
        //success
        container = $('#dialog-notifications-success');
    }
    else if (messagetype == 'error') {
        //error
        container = $('#dialog-notifications-error');
    }
    else {
        //other
        container = $('#dialog-notifications-success');
    }

    //we do not encode displayed message
    var htmlcode = '';
    if ((typeof message) == 'string') {
        htmlcode = '<p>' + message + '</p>';
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p>' + message[i] + '</p>';
        }
    }

    container.html(htmlcode);

    var isModal = (modal ? true : false);
    container.dialog({modal:isModal});
}


var barNotificationTimeout;
function displayBarNotification(message, messagetype, timeout) {
    clearTimeout(barNotificationTimeout);

    //types: success, error
    var cssclass = 'success';
    if (messagetype == 'success') {
        cssclass = 'success';
    }
    else if (messagetype == 'error') {
        cssclass = 'error';
    }
    //remove previous CSS classes and notifications
    $('#bar-notification')
        .removeClass('success')
        .removeClass('error');
    $('#bar-notification .content').remove();

    //we do not encode displayed message

    //add new notifications
    var htmlcode = '';
    if ((typeof message) == 'string') {
        htmlcode = '<p class="content">' + message + '</p>';
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p class="content">' + message[i] + '</p>';
        }
    }
    $('#bar-notification').append(htmlcode)
        .addClass(cssclass)
        .fadeIn('slow')
        .mouseenter(function ()
            {
                clearTimeout(barNotificationTimeout);
            });

    $('#bar-notification .close').unbind('click').click(function () {
        $('#bar-notification').fadeOut('slow');
    });

    //timeout (if set)
    if (timeout > 0) {
        barNotificationTimeout = setTimeout(function () {
            $('#bar-notification').fadeOut('slow');
        }, timeout);
    }
}

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}


function clickButton(e, buttonid) {
    var bt = document.getElementById(buttonid);
    if (typeof bt == 'object') {
        if (navigator.appName.indexOf("Netscape") > (-1)) {
            if (e.keyCode == 13) {
                bt.click();
                return false;
            }
        }

        if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
            if (event.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
}

$(function () {
    $(".animated_img").hover(function () {
        $("img.animated_img").animate({ left: '-5px' }, 200);
    });
    $(".animated_img").mouseout(function () {
        $("img.animated_img").animate({ left: '0px' }, 200);
    });

    jQuery('input[placeholder]').placeholder();
    
    if ($(".customerMenu").length > 0)
    {
        $(".customerMenu").click(function () {
            $(".customerMobileMenu").slideToggle(200);
            $(".customerMobileMenu").css("width", $(this).parent().width())
        });
    }

});


jQuery.fn.placeholder = function () {
    var i = document.createElement('input'),
		placeholdersupport = 'placeholder' in i;
    if (!placeholdersupport) {
        var inputs = jQuery(this);
        inputs.each(function () {
            var input = jQuery(this),
				text = input.attr('placeholder'),
				pdl = 0,
				height = input.outerHeight(),
				width = input.outerWidth(),
				placeholder = jQuery('<span class="phTips">' + text + '</span>');
            try {
                pdl = input.css('padding-left').match(/\d*/i)[0] * 1;
            } catch (e) {
                pdl = 5;
            }
            placeholder.css({ 'margin-left': -(width - pdl), 'min-height': height,'padding':'4px 0', 'line-height': height + "px", 'position': 'absolute', 'color': "#999", 'font-size': "12px" });
            placeholder.click(function () {
                input.focus();
            });
            if (input.val() != "") {
                placeholder.css({ display: 'none' });
            } else {
                placeholder.css({ display: 'inline' });
            }
            placeholder.insertAfter(input);
            input.keyup(function (e) {
                if (jQuery(this).val() != "") {
                    placeholder.css({ display: 'none' });
                } else {
                    placeholder.css({ display: 'inline' });
                }
            });
        });
    }
    return this;
};

function GetSMSValidateCode(phoneID, action) {
    var phoneexp = /^1\d{10}$/g;
    if ($("#" + phoneID).val().length != 11) {
        alert("请输入11位长度的手机号码");
        return;
    }

    if (!phoneexp.test($("#" + phoneID).val())) {
        alert("请输入正确的手机号码");
        return;
    }
    
    $.ajax({
        type: "Post",
        contentType: "Application/json",
        url: "/Plugins/Misc/" + action,
        data: '{"phone":"' + $("#" + phoneID).val() + '"}',
        dataType: "json",
        error: function (a, b, c) { alert(c); },
        success: function (obj) {
            $("#divSMSNotes").html("");
            switch (obj.Status) {
                case true:
                    $("#divSMSNotes").html("手机验证码已发送成功，请注意查收!");
                    jQuery("#btnGetSMSValidateCode").attr("disabled", "disabled");
                    var count = 60;
                    showTimer = function () {
                        jQuery("#btnGetSMSValidateCode").val("剩余" + count + "秒");
                        count--;
                        if (count < 0) {
                            clearInterval(timer);
                            $("#divSMSNotes").html("");
                            jQuery("#btnGetSMSValidateCode").removeAttr("disabled");
                            jQuery("#btnGetSMSValidateCode").val("重新获取手机验证码");
                        }
                    };
                    var timer = setInterval(showTimer, 1000);
                    break;
                case false:
                    if (obj.ErrorMsg == "")
                        $("#divSMSNotes").html("对不起，手机验证码发送失败!");
                    else
                        $("#divSMSNotes").html(obj.ErrorMsg);
                    break;
                default:
                    $("#divSMSNotes").html("请重新获取验证码!");
                    break;
            }
        }
    });
}
