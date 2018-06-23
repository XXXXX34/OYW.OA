 
var files_g = [];
function fileUploadSucceedCallBack(n) {
    files_g.push(n);
    $("#oa-file-ul").append('<li class="bg-success" data-FileID="' + n.FileID + '" data-FileName="' + n.FileName + '">' + n.FileName + '</li>');
}

function allFileUploadComplete() {

}

var totalCount = 0; //总条数
var totalPage = 0; //总页
var pageSize = 25; //默认25条 
var currentPage = 1; //当前页


var num_entries = 2;
var firstload = true;
var oa_request;
 

$(function () {

    NProgress.configure({ minimum: 0.7 });

    
    setframelayout();
    $("#oa-split").css('background', "url('/images/leftarrow.png') no-repeat center center");
    $('[data-toggle="tooltip"]').tooltip();
    bindLeftEvent();
    menuEvent();

    selectedMenu();

    bootbox.setDefaults("locale", "zh_CN");

    //toast-bottom-right表示下右、toast-bottom-center表示下中、toast-top-center表示上中
    toastr.options = {
        closeButton: false,
        debug: false,
        progressBar: true,
        positionClass: "toast-top-center",
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        timeOut: "2000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut"
    };
});
 
(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }

})(jQuery);

function selectedMenu() {
    var app = $.getUrlParam("app");
    var menu = $.getUrlParam("menu")
    $("#oa-app ul li a").each(function (i, n) {
        var $obj = $(this);
        if ($obj.attr("name") == app) {
            $obj.addClass("selected");
        }

    });
    $("#oa-left ul li div").each(function (i, n) {
        var $obj = $(this);
        if ($obj.attr("name") == menu) {
            $obj.addClass("selected");
        }

    });
}

var menu_G = [[]];

function loadMenus() {
     
}

function setFrame() {
    $("#container").css("position", "relative");
    var height = $(window).height() - 50;
    var ctrs = ["oa-app", "oa-left", "oa-split", "oa-right"];
    $(ctrs).each(function (i, n) {
        $("#" + n).css("height", height + "px");
    });
}

setFrame();

function menuEvent() {
    $("#oa-app ul li").delegate("a", "click", function () {
        $("#oa-app ul li a").removeClass("selected");
        $(this).addClass("selected");
    });

    $("#oa-left ul li").delegate("div", "click", function () {
        $("#oa-left ul li div").removeClass("selected");
        $(this).addClass("selected");
    });
}

function setframelayout() {
    $("#container").css("position", "");
    var topRow = [];
    var middleRow = [{ id: "oa-app", width: "50", height: "fill" }, { id: "oa-left", width: "190", height: "fill" }, { id: "oa-split", width: "10", height: "fill" }, { id: "oa-right", width: "fill", height: "fill" }];
    var bottomRow = [];
    $.layoutMY(topRow, middleRow, bottomRow, window, 0, 50);
    $("#oa-left").css("z-index", "-10");

    $("#oa-app").niceScroll({ cursorcolor: "#373d41" });


    try {
        callbackParentResizeChange();
    }
    catch (e) {

    }
}

function setlayout() {
    $("#oa-right").css({ overflow: 'hidden', visibility: 'visible' });
    var topRow1 = [{ id: "oa-searchBar", width: "fill", height: "auto" }];
    var middleRow2 = [{ id: "oa-list", width: "fill", height: "fill" }];
    var bottomRow3 = [{ id: "oa-page", width: "fill", height: "35" }];
    $.layoutMY(topRow1, middleRow2, bottomRow3, $("#oa-right"), 0, 0, 3, 2);
}

window.onresize = function () {
    setframelayout();
    setlayout();
}

function setlayoutHidLeft() {
    $("#container").css("position", "");
    var topRow = [];
    var middleRow = [{ id: "oa-app", width: "50", height: "fill" }, { id: "oa-left", width: "0", height: "fill" }, { id: "oa-split", width: "10", height: "fill" }, { id: "oa-right", width: "fill", height: "fill" }];
    var bottomRow = [];
    $.layoutMY(topRow, middleRow, bottomRow, window, 0, 50);
    $("#oa-left").css("z-index", "-10");
    setlayout();
}

function bindLeftEvent() {
    $("#oa-split").click(function () {
        var leftW = $("#oa-left").width();
        if (leftW <= 0) {
            setframelayout();
            setlayout();
            $(this).css('background', "url('/images/leftarrow.png') no-repeat center center");
            $("#oa-left").css('overflow-y', 'auto');
        }
        else {
            setlayoutHidLeft();
            $(this).css('background', "url('/images/rightarrow.png') no-repeat center center");

            $("#oa-left").css('overflow', 'hidden');
        }
        try {
            //callbackSplitClick();
            callbackParentResizeChange();
        }
        catch (ex) {

        }
    });
}

function formatModel(parModel) {
    for (var prop in parModel) {
        if (parModel[prop] == null)
            parModel[prop] = '';
    }
}

function clearModel(parModel) {
    for (var prop in parModel) {
        parModel[prop] = '';
    }
}

String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    //var reg = new RegExp("({[" + i + "]})", "g");//这个在索引大于9时会有问题，谢谢何以笙箫的指出
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}

function timeStamp2String(time) {
    if (time == null) return "";
    time = time.replace("/Date(", "").replace(")/", "");
    var datetime = new Date();
    datetime.setTime(time);
    var year = datetime.getFullYear();
    var month = datetime.getMonth() + 1 < 10 ? "0" + (datetime.getMonth() + 1) : datetime.getMonth() + 1;
    var date = datetime.getDate() < 10 ? "0" + datetime.getDate() : datetime.getDate();
    var hour = datetime.getHours() < 10 ? "0" + datetime.getHours() : datetime.getHours();
    var minute = datetime.getMinutes() < 10 ? "0" + datetime.getMinutes() : datetime.getMinutes();
    var second = datetime.getSeconds() < 10 ? "0" + datetime.getSeconds() : datetime.getSeconds();
    return year + "-" + month + "-" + date + " " + hour + ":" + minute + ":" + second;
}
function timeStamp2StringShort(time) {
    if (time == null) return "";
    var res = timeStamp2String(time);
    if (res.length >= 10)
        return res.substring(0, 10);
    return "";
}


//限制字符数
function maxInput(obj, len) {
    var v = obj.value;
    if (v.myLength() > len) {
        alert("最大只能输入" + len + "个字符");
    }
}

function toFix(obj, num) {
    if (obj.value.length != 0) {
        obj.value = parseFloat(obj.value).toFixed(num);
    }
}

//非负数
function positiveFloat(event, obj) {
    try {
        event = window.event || event;
        if (event.keyCode == 37 | event.keyCode == 39) {
            return;
        }
        obj.value = obj.value.replace(/[^\d.]/g, "");
        obj.value = obj.value.replace(/[0]\d/g, obj.value.substr(1, obj.value.length - 1));
        obj.value = obj.value.replace(/^\./g, "");
        obj.value = obj.value.replace(/\.{2,}/g, ".");
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    }
    catch (ex) {

    }
}

function positiveInt(obj) {
    var v = obj.value;
    if (v.length == 1) {

        obj.value = v.replace(/[^0-9]/g, '');
    }
    else {
        obj.value = v.replace(/\D/g, '');
    }
}

