
$(function () {
    JPCheckout.init();
    //JPCheckout.showPayedSuccess(22);
});


var JPCheckout = {
    init: function () {
        this.initShippingAddress();
        this.initShippingMethods();
        this.initPaymentMethods();
        this.initUseCoupon();
    },

    initShippingAddress: function () {
        $(".option-list li").hover(function () {
            $(this).find(".op-btns").show();
        },
		function () {
		    $(this).find(".op-btns").hide();
		})

        $(".select-shipping-address li label").bind("click", function () {
            $(this).parents("ul").find("label").removeClass("selected");
            $(this).addClass("selected");
            var id = $(this).attr("fid");
            JPCheckout.changeShippingAddress(id);
        })

        $(".del-consignee").click(function () {
            var id = $(this).attr("fid");
            JPCheckout.deleteAddress(id);
        })

        $(".setdefault-consignee").click(function () {
            var id = $(this).attr("fid");
            JPCheckout.setDefaultAddress(id);
        })

        $(".addOrEdit-consignee").click(function () {
            var param = typeof ($(this).attr("fid")) == "undefined" ? "" : "?addressId=" + $(this).attr("fid");
            var isNew = param.length > 0 ? false : true;
            JPCheckout.showWaiting(true);
            $.get("/Plugins/Misc/ConsigneeAddOrEditForm/" + param, { stamp: Math.random() }, function (html) {
                JPCheckout.hideWaiting();
                var form = $(html);
                $.validator.unobtrusive.parse(form);
                form.dialog({
                    modal: true,
                    width: document.body.clientWidth < 650 ? document.body.clientWidth * 0.9 : 600,
                    height: 330,
                    title: isNew ? "新增收货地址" : "编辑收货地址",
                    close: function () { $(this).remove(); }
                });
                form.submit(function () {
                    //表单的处理
                    if (form.valid()) {
                        JPCheckout.showWaiting(false);
                        $.ajax({
                            cache: false,
                            url: form.attr("action"),
                            type: 'post',
                            data: form.serialize(),
                            error: function () {
                                alert("保存失败！");
                            },
                            success: function (data) {
                                if (data.Status) {
                                    JPCheckout.refreshAddress();
                                    form.dialog('close')
                                }
                                else {
                                    if ($(".ajax_form_errormsg").length > 0) {
                                        $(".ajax_form_errormsg").remove();
                                    }
                                    form.prepend("<div class='ajax_form_errormsg'>" + data.ErrorMsg + "</div>")
                                }
                            },
                            complete: function () {
                                JPCheckout.hideWaiting();
                            }
                        })
                    }
                    return false;
                })
            })

        })
    },

    initShippingMethods: function () {
        $(".shipping-method li label").bind("click", function () {
            $(this).parents("ul").find("label").removeClass("selected");
            $(this).addClass("selected");
            var id = $(this).attr("for");
            var val = $("#" + id).val();
            if ($(this).attr("isSelfPick") == "false") {
                $(this).parent().parent().parent().parent().find('.selfpick_shipment').addClass('hidden');
            }
            else {
                $(this).parent().parent().parent().parent().find('.selfpick_shipment').removeClass("hidden")
            }
            var cartItemId = $(this).attr("otherfor");
            if (!cartItemId) {
                cartItemId = 0;
            }
            JPCheckout.saveShippingMethod(val, cartItemId);
        })
    },

    initPaymentMethods: function () {
        $("#UseRewardPoints").change(function () {
            var paymentMethod = $("input[name='paymentmethod']:checked").val();
            var useRewardPoints = $(this).is(':checked');
            JPCheckout.savePaymentMethod(paymentMethod, useRewardPoints);
        })
        $("input[name='paymentmethod']").change(function () {
            var paymentMethod = $("input[name='paymentmethod']:checked").val();
            var useRewardPoints = $(this).is(':checked');
            JPCheckout.savePaymentMethod(paymentMethod, useRewardPoints);
        })
    },

    initUseCoupon: function () {
        $(".order-coupon .coupon-title a").click(function () {
            var wrap = $(this).parent().parent().find(".coupon-wrap");
            if (wrap.hasClass("hidden")) {
                wrap.removeClass("hidden");
                $(this).addClass("coupon-title-active");
            }
            else {
                wrap.addClass("hidden");
                $(this).removeClass("coupon-title-active");
            }
        });

        $("#applydiscountcouponcode").click(function () {
            var discountcouponcode = $("#discount-coupon-code").val();
            JPCheckout.useDiscountCoupon(discountcouponcode, function (result) {
                $("#discountCouponCode").html(result.Message);
                $("#removediscountcouponcode").removeClass("hidden");
            });
        });

        $("#removediscountcouponcode").click(function () {
            JPCheckout.removeDiscountCoupon(function (result) {
                $("#discountCouponCode").html("");
                $("#removediscountcouponcode").addClass("hidden");
            });
        });

        $("#applygiftcardcouponcode").click(function () {
            var giftcardcouponcode = $("#gift-card-coupon-code").val();
            JPCheckout.useGiftCard(giftcardcouponcode);

        });

    },

    deleteAddress: function (addressid) {
        if (confirm('您确定要删除该收货地址吗？')) {
            JPCheckout.showWaiting(true);
            $.ajax({
                cache: false,
                url: '/Plugins/Misc/AddressDelete/' + addressid,
                type: 'post',
                error: function () {
                    alert("删除失败！");
                },
                success: function (data) {
                    if (data.Status) {
                        JPCheckout.refreshAddress();
                    }
                    else {
                        alert(data.ErrorMsg)
                    }
                },
                complete: function () {
                    JPCheckout.hideWaiting();
                }
            })
        }
    },

    setDefaultAddress: function (addressid) {
        JPCheckout.showWaiting(true);
        $.ajax({
            cache: false,
            url: '/Plugins/Misc/SetDefaultShippingAddress/' + addressid,
            type: 'post',
            error: function () {
                alert("设置默认地址失败！");
                JPCheckout.hideWaiting();
            },
            success: function (data) {
                if (data.Status) {
                    JPCheckout.refreshAddress();
                    JPCheckout.changeShippingAddress(addressid);
                }
                else {
                    alert(data.ErrorMsg)
                }
            }
        })
    },

    saveShippingMethod: function (shippingoption, cartItemId) {
        JPCheckout.showWaiting(true);
        $.ajax({
            cache: false,
            url: '/Plugins/Misc/SaveShippingMethod',
            type: 'post',
            data: { "shippingoption": shippingoption, "cartItemId": cartItemId },
            error: function () {
                alert("保存配送方式失败！");
            },
            success: function (data) {
                if (data.Status) {
                    JPCheckout.refreshOrderTotals();
                }
                else {
                    alert(data.ErrorMsg)
                }
            },
            complete: function () {
                JPCheckout.hideWaiting();
            }
        })
    },

    savePaymentMethod: function (paymentmethod, useRewardPoints) {
        JPCheckout.showWaiting(true);
        $.ajax({
            cache: false,
            url: '/Plugins/Misc/SavePaymentMethod',
            type: 'post',
            data: { "paymentmethod": paymentmethod, "useRewardPoints": useRewardPoints },
            error: function () {
                alert("保存支付方式失败！");
            },
            success: function (data) {
                if (data.Status) {
                    JPCheckout.refreshOrderTotals();
                }
                else {
                    alert(data.ErrorMsg)
                }
            },
            complete: function () {
                JPCheckout.hideWaiting();
            }
        })
    },

    useDiscountCoupon: function (discountcouponcode, callback) {
        JPCheckout.showWaiting(true);
        $.ajax({
            cache: false,
            url: '/Plugins/Misc/UseDiscountCoupon',
            type: 'post',
            data: { "discountcouponcode": discountcouponcode },
            error: function () {
                alert("使用折扣码失败！");
            },
            success: function (data) {
                if (data.Status) {
                    callback(data);
                    JPCheckout.refreshOrderTotals();
                }
                else {
                    alert(data.ErrorMsg)
                }
            },
            complete: function () {
                JPCheckout.hideWaiting();
            }
        })
    },

    useGiftCard: function (giftcardcouponcode) {
        JPCheckout.showWaiting(true);
        $.ajax({
            cache: false,
            url: '/Plugins/Misc/UseGiftCard',
            type: 'post',
            data: { "giftcardcouponcode": giftcardcouponcode },
            error: function () {
                alert("使用礼品卡失败！");
            },
            success: function (data) {
                if (data.Status) {
                    JPCheckout.refreshOrderTotals();
                }
                else {
                    alert(data.ErrorMsg)
                }
            },
            complete: function () {
                JPCheckout.hideWaiting();
            }
        })
    },

    removeDiscountCoupon: function (callback) {
        JPCheckout.showWaiting(true);
        $.ajax({
            cache: false,
            url: '/Plugins/Misc/RemoveDiscountCoupon',
            type: 'post',
            error: function () {
                alert("取消折扣码失败！");
            },
            success: function (data) {
                if (data.Status) {
                    callback(data);
                    JPCheckout.refreshOrderTotals();
                }
                else {
                    alert(data.ErrorMsg)
                }
            },
            complete: function () {
                JPCheckout.hideWaiting();
            }
        })
    },

    refreshAddress: function () {
        $.get("/Plugins/Misc/OpcShippingAddress", { stamp: Math.random() }, function (html) {
            $("#checkout-shipping-load").html(html);
            JPCheckout.initShippingAddress();
        })
    },

    refreshOrderTotals: function () {
        $.get("/Plugins/Misc/GetOrderTotals?isEditable=false", { stamp: Math.random() }, function (html) {
            $("#checkout-OrderTotals").html(html);
        })
    },

    changeShippingAddress: function (id) {
        JPCheckout.showWaiting(true);
        //$("#checkout-shipping-method-load").html("");
        $.get("/Plugins/Misc/ChangeShippingAddress/" + id, { stamp: Math.random() }, function (html) {
            $("#checkout-shipping-method-load").html(html);
            JPCheckout.initShippingMethods();
            JPCheckout.refreshOrderTotals();
            JPCheckout.hideWaiting();
        })
    },

    showWaiting: function (hideWidget) {
        if ($(".dialog_waiting").length == 0) {
            $("<div class='dialog_waiting'><img src='http://misc.360buyimg.com/user/purchase/css/i/loading04.gif'/></div>").dialog({
                width: 100,
                height: 130,
                modal: true,
                closeOnEscape: false,
                dialogClass: "dialog-waiting",
                open: function () {
                    if (hideWidget || hideWidget == undefined) {
                        $(".ui-widget-overlay").css("opacity", "0");
                    }
                },
                close: function () { $(this).remove(); }
            });
        }
    },

    showPayedSuccess: function (orderid, paylink) {
        $("<div class='wangyin-payed-success'><div class='mb20'>请您在新打开的支付平台页面进行支付，支付完成前请不要关闭该窗口</div><div class='wps-button'><a class='ui-button ui-button-gray' href='/orderinfo/" + orderid + "'>已完成支付</a><a class='ui-button ui-button-gray' href='/支付遇到问题' target='_blank'>支付遇到问题</a></div><div class='wps-other'><a href='" + paylink + "' target='_blank'>重新付款</a></div></div>").dialog({
            width: 480,
            height: 250,
            title: "登录平台支付",
            modal: true,
            resizable: false,
            draggable: false,
            closeOnEscape: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", $(this).parent()).hide();
            },
            close: function () { $(this).remove(); }
        });
    },

    hideWaiting: function () {
        if ($(".dialog_waiting").length > 0) {
            $(".dialog_waiting").dialog('close');
        }
    },

    confirmOrder: function () {
        JPCheckout.showWaiting(true);
        var termOfServiceOk = true;
        if ($('#termsofservice').length > 0) {
            if (!$('#termsofservice').is(':checked')) {
                JPCheckout.hideWaiting();
                $("#terms-of-service-warning-box").dialog();
                termOfServiceOk = false;
            } else {
                termOfServiceOk = true;
            }
        }
        if (termOfServiceOk) {
            var postResult = {};
            $.ajax({
                cache: false,
                url: "/Plugins/Misc/OpcConfirmOrder/",
                type: 'post',
                async: false,
                data: $("#checkout-confirm-order-load").find("form").serialize(),
                success: function (response) {
                    postResult = response;
                    if (response.error != undefined && response.error == 1) {
                        if ((typeof response.message) == 'string') {
                            alert(response.message);
                        } else {
                            alert(response.message.join("\n"));
                        }
                        return false;
                    }
                    else if (response.success != undefined && response.success == false) {
                        if ((typeof response.message) == 'string') {
                            alert(response.message);
                        } else {
                            alert(response.message.join("\n"));
                        }
                        return false;
                    }
                },
                complete: function () {
                    JPCheckout.hideWaiting();
                },
                error: function (html) {
                    alert("订单提交失败！");
                }
            });

            if (postResult.redirect) {
                //location.href = response.redirect;
                JPCheckout.showPayedSuccess(postResult.orderID, postResult.redirect);
                window.open(postResult.redirect);
                return;
            }
            if (postResult.success) {
                window.location = "/checkout/completed/";
            }

        } else {

            return false;
        }
    },

    chooseShop: function (vendorId, companyId) {
        JPCheckout.showWaiting(true);
        var param = "";
        if (vendorId != undefined) {
            param = "vendorId=" + vendorId;
        }
        if (companyId != undefined) {
            param = "companyId=" + companyId;
        }
        $.get("/Plugins/Misc/ChooseShop?" + param, { stamp: Math.random() }, function (html) {
            JPCheckout.hideWaiting();
            var form = $(html);
            form.dialog({
                modal: true,
                width: document.body.clientWidth < 850 ? document.body.clientWidth * 0.9 : 800,
                height: 400,
                title: "选择自提点",
                close: function () { $(this).remove(); }
            });
            form.submit(function () {
                JPCheckout.showWaiting(false);
                $.ajax({
                    cache: false,
                    url: form.attr("action"),
                    type: 'post',
                    data: form.serialize(),
                    error: function () {
                        alert("保存失败！");
                    },
                    success: function (data) {
                        $("#checkout-shipping-method-load").html(data);
                        JPCheckout.initShippingMethods();
                        form.dialog('close');
                    },
                    complete: function () {
                        JPCheckout.hideWaiting();
                    }
                })

                return false;
            })
        })
    }
}