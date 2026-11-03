
var selectedIds = [];
var currentProductID = 0;
var K ;

$(document).ready(function () {
    K = window.KindEditor;
    //search button
    $('#search-products').click(function () {
        //search
        var grid = $('#products-grid').data('kendoGrid');
        grid.dataSource.page(1); //new search. Set page size to 1
        //grid.dataSource.read(); we already loaded the grid above using "page" function
        //clear selected checkboxes
        $('.checkboxGroups').attr('checked', false).change();
        selectedIds = [];
        $(".search-more").slideUp(100);
        return false;
    });

    $("#SearchProductName").keydown(function (event) {
        if (event.keyCode == 13) {
            $("#search-products").click();
            return false;
        }
    });
    $("#GoDirectlyToSku").keydown(function (event) {
        if (event.keyCode == 13) {
            $("#go-to-product-by-sku").click();
            return false;
        }
    });

    $("#search-more").click(function () {
        $(".search-more").slideToggle(100);
    })

    //$(".top-toolbar").mouseleave(function () {
    //    $(".search-more").slideUp(100);
    //})

    //delete selected
    $('#delete-selected').click(function (e) {

        e.preventDefault();

        var postData = {
            selectedIds: selectedIds
        };

        $.ajax({
            cache: false,
            type: "POST",
            url: "/Admin/Product/DeleteSelected",
            data: postData,
            complete: function (data) {
                //reload grid
                var grid = $('#products-grid').data('kendoGrid');
                grid.dataSource.read();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            },
            traditional: true
        });
        return false;
    });

    $(".categoriesList a").click(function () {
        $(".categoriesList a").removeClass("list-group-item-info");
        $(this).addClass("list-group-item-info");
        $("#SearchCategoryId").val($(this).attr("cid"));
        $("#search-products").click();
    })

    //缺少信息 查询
    $(".lackType a").click(function () {
        $(".lackType a").removeClass("list-group-item-info");
        $(this).addClass("list-group-item-info");
        $("#SearchLackType").val($(this).attr("type"));
        $("#search-products").click();
    })

    //排序
    $(".orderby-menu a").click(function () {
        $(".orderby-menu a").removeClass("list-group-item-info");
        $(this).addClass("list-group-item-info");
        $("#SortType").val($(this).attr("orderby"));
        $("#search-products").click();
    })

    //已被删除的商品
    $("#searchDeleted-btn").click(function () {
        var deleted = $("#SearchDeleted").val();
        if (deleted == "true") {
            $(this).removeClass("btn-danger");
            $(this).addClass("btn-primary");
            $("#SearchDeleted").val(false);
        }
        else {
            $(this).addClass("btn-danger");
            $(this).removeClass("btn-primary");
            $("#SearchDeleted").val(true);
        }
        $("#search-products").click();
    })

    $(".categoriesTitle").click(function () {
        $(".categoriesList .items").slideToggle(100);
    })

    //wire up checkboxes. 
    $('#products-grid').on('change', 'input[type=checkbox][id!=mastercheckbox]', function (e) {
        var $check = $(this);
        if ($check.is(":checked") == true) {
            var checked = jQuery.inArray($check.val(), selectedIds);
            if (checked == -1) {
                //add id to selectedIds.  
                selectedIds.push($check.val());
            }
        }
        else {
            var checked = jQuery.inArray($check.val(), selectedIds);
            if (checked > -1) {
                //remove id from selectedIds.  
                selectedIds = $.grep(selectedIds, function (item, index) {
                    return item != $check.val();
                });
            }
        }
        updateMasterCheckbox();
    });


    $("#recover-selected").click(function (e) {
        UpdateSelected(e, "Recover");
    })

    $("#published-selected").click(function (e) {
        UpdateSelected(e, "Published");
    })

    $("#unpublished-selected").click(function (e) {
        UpdateSelected(e, "UnPublished");
    })

    $("#productSave").click(function () {
        ShowWaiting(false);
        $("#FullDescription").html(window.editor.html());
        var form = $("#product-form");
        $.ajax({
            cache: false,
            url: "/Admin/Plugin/Misc/Product/Save",
            type: 'post',
            data: form.serialize(),
            error: function () {
                alert("保存失败！");
            },
            success: function (data) {
                if (data.status) {
                    refreshProductCount();
                    alert("保存成功!");
                }
                else {
                    alert(data.message);
                }
            },
            complete: function () {
                HideWaiting();
            }
        })
    })

    refreshProductCount();
});

function refreshProductCount() {
    $.post("/Admin/Plugin/Misc/Product/GetProductCount?deleted=" + $("#SearchDeleted").val(), function (data) {
        $(".categoriesList .badge").html("");
        $(".lackType .badge").html("");

        $(".lackType a").each(function () {
            var type = $(this).attr("type").toLocaleLowerCase();
            if (type != "") {
                for (var p in data) {
                    if (p.toLocaleLowerCase().indexOf(type) != -1) {
                        $(this).find(".badge").html(data[p]);
                    }
                }
            }
        })

        $.each(data.CategoriesCount, function (id, cat) {
            $(".categoriesList a[cid=" + cat.CategoryId + "]").find(".badge").html(cat.Count);
        })

        $("#searchDeleted-btn").find(".badge").html(data.DeletedCount);
        $(".categoriesList a[cid='0']").find(".badge").html(data.AllCount);
        $(".lackType a[type='']").find(".badge").html(data.AllCount);
    })
}

function UpdateSelected(e, action) {
    e.preventDefault();

    var postData = {
        selectedIds: selectedIds,
        action: action
    };

    $.ajax({
        cache: false,
        type: "POST",
        url: "/Admin/Plugin/Misc/Product/UpdateSelected",
        data: postData,
        complete: function (data) {
            //reload grid
            var grid = $('#products-grid').data('kendoGrid');
            grid.dataSource.read();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        },
        traditional: true
    });
    return false;
}

function additionalData() {
    return {
        SearchProductName: $('#SearchProductName').val(),
        SearchCategoryId: $('#SearchCategoryId').val(),
        SearchIncludeSubCategories: $('#SearchIncludeSubCategories').is(':checked'),
        SearchManufacturerId: $('#SearchManufacturerId').val(),
        SearchStoreId: "",
        SearchWarehouseId: "",
        SearchVendorId: $('#SearchVendorId').val(),
        SearchProductTypeId: "",
        LackType: $("#SearchLackType").val(),
        SortType: $("#SortType").val(),
        SearchDeleted : $("#SearchDeleted").val()
    };
}

function onDataBound(e) {
    $('#products-grid input[type=checkbox][id!=mastercheckbox]').each(function () {
        var currentId = $(this).val();
        var checked = jQuery.inArray(currentId, selectedIds);
        //set checked based on if current checkbox's value is in selectedIds.  
        $(this).attr('checked', checked > -1);
    });

    $("#products-grid tr td").click(function () {
        var checkbox = $(this).find("input[type=checkbox]");
        if (checkbox.length > 0) {
            if (!checkbox.is(":focus")) {
                checkbox.attr('checked', !checkbox.is(':checked')).change();
            }
            return;
        }
        $("#products-grid tr").removeClass("activae");
        var pid = $(this).parent().find("a").attr("pid");
        showEdit(pid);
        $(this).parent().addClass("activae");
    })

    updateMasterCheckbox();

    $("#mastercheckbox").unbind("click")
    $('#mastercheckbox').click(function () {
        $('.checkboxGroups').attr('checked', $(this).is(':checked')).change();
    });
    refreshProductCount();
}

function updateMasterCheckbox() {
    var numChkBoxes = $('#products-grid input[type=checkbox][id!=mastercheckbox]').length;
    var numChkBoxesChecked = $('#products-grid input[type=checkbox][checked][id!=mastercheckbox]').length;
    $('#mastercheckbox').attr('checked', numChkBoxes == numChkBoxesChecked && numChkBoxes > 0);
}

function showEdit(id) {
    ShowWaiting(false);
    $.post("/Admin/Plugin/Misc/Product/GetProductJson/" + id, function (data) {
        var infoTemplate = Handlebars.compile($("#infoTemplate").html());
        var seoTemplate = Handlebars.compile($("#seoTemplate").html());
        var fullDescriptionTemplate = Handlebars.compile($("#fullDescriptionTemplate").html());
        var compileHtml = infoTemplate(data);
        $('#productInfo').html(compileHtml);
        compileHtml = seoTemplate(data);
        $("#productSeo").html(compileHtml);
        compileHtml = fullDescriptionTemplate(data);
        $("#productFullDescription").html(compileHtml);

        $("#VendorId").val(data.VendorId);
        $("#ManageInventoryMethodId").val(data.ManageInventoryMethodId);
        $("#WarehouseId").val(data.WarehouseId);
        $("#LowStockActivityId").val(data.LowStockActivityId);
        $("#BackorderModeId").val(data.BackorderModeId);

        $("#pnlMultipleWarehouses .warehouse_used").change(function () {
            var warehouseId = $(this).attr("vid");
            if ($('#warehouse_used_' + warehouseId).is(':checked')) {
                $('#warehouse_qty_' + warehouseId).prop('disabled', false);
                $('#warehouse_reserved_' + warehouseId).prop('disabled', false);
            }
            else {
                $('#warehouse_qty_' + warehouseId).prop('disabled', true);
                $('#warehouse_reserved_' + warehouseId).prop('disabled', true);
            }
        })

        $(".kendoNumeric input").kendoNumericTextBox({
            format: "#.0000",
            decimals: 4
        });

        $(".kendoInt input").kendoNumericTextBox({
            format: "#",
            decimals: 0
        });

        $(".kendoDatePicker input").kendoDatePicker();

        currentProductID = id;
        initInfo();
        initTags();
        initKindEditor();
        initCategories();
        initManufactures();
        initPictures();
        initSpecification();
        initProductAttributeMapping();
        initAttributeCombinations();
        HideWaiting();
    })
}

function initKindEditor() {
    window.editor = K.create('#FullDescription', {
        width: '100%',
        height: '700px',
        cssPath: '/Content/kindeditor/plugins/code/prettify.css',
        uploadJson: '/Content/kindeditor/asp.net/upload_json.ashx',
        fileManagerJson: '/Content/kindeditor/asp.net/file_manager_json.ashx',
        allowFileManager: true,
        afterCreate: function () {
            var self = this;
            K.ctrl(document, 13, function () {
                self.sync();
                K('form[name=example]')[0].submit();
            });
            K.ctrl(self.edit.doc, 13, function () {
                self.sync();
                K('form[name=example]')[0].submit();
            });
        }
    });
}