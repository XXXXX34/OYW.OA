$(function () {

    $.fn.extralWidth = function () {
        var w = 0;

        var pl = parseInt($(this).css('padding-left'), 10);
        if (pl) w += pl;

        var pr = parseInt($(this).css('padding-right'), 10);
        if (pr) w += pr;

        var blw = parseInt($(this).css('border-left-width'), 10);
        if (blw) w += blw;

        var brw = parseInt($(this).css('border-right-width'), 10);
        if (brw) w += brw;

        return w;
    }
    $.fn.extralHeight = function () {
        var h = 0;

        var pt = parseInt($(this).css('padding-top'), 10);
        if (pt) h += pt;

        var pb = parseInt($(this).css('padding-bottom'), 10);
        if (pb) h += pb;

        var btw = parseInt($(this).css('border-top-width'), 10);
        if (btw) h += btw;

        var bbw = parseInt($(this).css('border-bottom-width'), 10);
        if (bbw) h += bbw;

        return h;
    }
});


var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i) ? true : false;
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i) ? true : false;
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i) ? true : false;
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i) ? true : false;
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Windows());
    }
};


(function ($) {
    $.layoutMY = function (topRow, middleRow, bottomRow, containerSender, left, top, subtractWidth, subtractHeight) {
        subtractWidth = (subtractWidth == null || subtractWidth == undefined) ? 0 : parseInt(subtractWidth);
        subtractHeight = (subtractHeight == null || subtractHeight == undefined) ? 0 : parseInt(subtractHeight);
        var win_width = $(containerSender).width() - subtractWidth - left;
        var win_height = $(containerSender).height() - subtractHeight - top;

        if (containerSender == window) {
            if (isMobile.any()) {
                if (win_width < 1224) {
                    win_width = 1224;
                }
                if (win_height < 900) {
                    win_height = 900;
                }
            }
        }

        var fixWidth = 0;
        var fillWidthCount = 0;
        var fillWidth = 0;
        var leftPosition = 0;

        var dicWidthExtral = [[]];
        var dicHeightExtral = [[]];
        $(topRow).each(function (i, n) {
            if (n == undefined) return;
            if ($("#" + n.id).css("display") == "none") { return };
            var tempEW = parseInt($("#" + n.id).extralWidth());
            var tempEH = parseInt($("#" + n.id).extralHeight());
            dicWidthExtral[n.id] = tempEW;
            dicHeightExtral[n.id] = tempEH;
            if (n.width == "fill") {
                fillWidthCount++;
            }
            else {
                fixWidth += parseInt(n.width) + tempEW;
            }
        });

        fillWidth = (win_width - fixWidth) / fillWidthCount;

        $(topRow).each(function (i, n) {
            if (n == undefined) return;
            if ($("#" + n.id).css("display") == "none") return;
            var widthTemp = n.width;
            var realWidthTemp = 0;
            var realHeight = parseInt(dicHeightExtral[n.id]);
            if (n.width == "fill") {
                widthTemp = parseInt(fillWidth);
                realWidthTemp = parseInt(fillWidth) - parseInt(dicWidthExtral[n.id]);
            }
            else {
                widthTemp = parseInt(n.width);
                realWidthTemp = parseInt(n.width) - parseInt(dicWidthExtral[n.id]);
            }
            var eleHeight = n.height;
            if (eleHeight = "auto") {
                $("#" + n.id).css({ height: "auto" });
                eleHeight = $("#" + n.id).height();
            }
            else {
                eleHeight = n.height - realHeight;
            }
            var overFlowStr = $("#" + n.id).css("overflow");
            if (overFlowStr == 'visible') {
                overFlowStr = 'auto';
            }
            $("#" + n.id).css({ position: "absolute" }).stop().css({ width: (realWidthTemp) + "px", height: (eleHeight) + "px", left: leftPosition + left + "px", top: top + "px", overflow: overFlowStr });
            //$("#" + n.id).getNiceScroll().resize();
            leftPosition += parseInt(widthTemp);
        });

        dicWidthExtral = [[]];
        dicHeightExtral = [[]];
        fixWidth = 0;
        fillWidthCount = 0;
        fillWidth = 0;
        leftPosition = 0;
        $(middleRow).each(function (i, n) {
            if (n == undefined) return;
            if ($("#" + n.id).css("display") == "none") return;
            var tempEW = parseInt($("#" + n.id).extralWidth());
            var tempEH = parseInt($("#" + n.id).extralHeight());
            dicWidthExtral[n.id] = tempEW;
            dicHeightExtral[n.id] = tempEH;
            if (n.width == "fill") {
                fillWidthCount++;
            }
            else {
                fixWidth += parseInt(n.width) + tempEW;
            }
        });
        var topRow_height = 0;
        var bottomRow_height = 0;
        if (topRow.length != 0 && $("#" + topRow[0].id).css("display") != "none") {
            if (topRow[0].height = "auto") {
                var eleID = "#" + topRow[0].id;
                $(eleID).css({ height: "auto" });
                topRow_height = parseInt($(eleID).height()) + parseInt($(eleID).extralHeight());
                topAuto = true;
            }
            else {
                topRow_height = parseInt(topRow[0].height);
            }
        }
        if (bottomRow.length != 0 && $("#" + bottomRow[0].id).css("display") != "none") {
            if (bottomRow[0].height == "fill") {
                var eleID = "#" + middleRow[0].id;
                var eleIDBottom = "#" + bottomRow[0].id;
                bottomRow_height = win_height - (parseInt(middleRow[0].height) + parseInt($(eleID).extralHeight() + parseInt($(eleIDBottom).extralHeight())) + topRow_height);
            }
            else {
                bottomRow_height = parseInt(bottomRow[0].height);
            }
        }

        fillWidth = (win_width - fixWidth) / fillWidthCount;
        $(middleRow).each(function (i, n) {
            if (n == undefined) return;
            if ($("#" + n.id).css("display") == "none") return;
            var widthTemp = n.width;
            var realWidthTemp = 0;
            var realHeight = parseInt(dicHeightExtral[n.id]);
            if (n.width == "fill") {
                widthTemp = parseInt(fillWidth);
                realWidthTemp = parseInt(fillWidth) - parseInt(dicWidthExtral[n.id]);
            }
            else {
                widthTemp = parseInt(n.width);
                realWidthTemp = parseInt(n.width) - parseInt(dicWidthExtral[n.id]);
            }
            var eleheight = win_height - topRow_height - bottomRow_height - realHeight;
            var overFlowStr = $("#" + n.id).css("overflow");

            if (overFlowStr == 'visible') {
                overFlowStr = 'auto';
            }
            $("#" + n.id).css({ position: "absolute" }).stop().css({ width: (realWidthTemp) + "px", height: (eleheight) + "px", left: leftPosition + left + "px", top: (parseInt(topRow_height) + top) + "px", overflow: overFlowStr });
            //$("#" + n.id).getNiceScroll().resize();
            leftPosition += parseInt(widthTemp);
        });

        dicWidthExtral = [[]];
        dicHeightExtral = [[]];
        fixWidth = 0;
        fillWidthCount = 0;
        fillWidth = 0;
        leftPosition = 0;
        $(bottomRow).each(function (i, n) {
            if (n == undefined) return;
            if ($("#" + n.id).css("display") == "none") return;
            var tempEW = parseInt($("#" + n.id).extralWidth());
            var tempEH = parseInt($("#" + n.id).extralHeight());
            dicWidthExtral[n.id] = tempEW;
            dicHeightExtral[n.id] = tempEH;
            if (n.width == "fill") {
                fillWidthCount++;
            }
            else {
                parseInt(n.width) + tempEW;
            }
        });
        fillWidth = (win_width - fixWidth) / fillWidthCount;
        $(bottomRow).each(function (i, n) {
            if (n == undefined) return;
            if ($("#" + n.id).css("display") == "none") return;
            var widthTemp = n.width;
            var realWidthTemp = 0;
            var realHeight = parseInt(dicHeightExtral[n.id]);
            if (n.width == "fill") {
                widthTemp = parseInt(fillWidth);
                realWidthTemp = parseInt(fillWidth) - parseInt(dicWidthExtral[n.id]);
            }
            else {
                widthTemp = parseInt(n.width);
                realWidthTemp = parseInt(n.width) - parseInt(dicWidthExtral[n.id]);
            }
            if (n.height == "fill") {
                var heightBottom = bottomRow_height;
            }
            else {
                var heightBottom = (parseInt(n.height) - realHeight);
            }
            var overFlowStr = $("#" + n.id).css("overflow");
            if (overFlowStr == 'visible') {
                overFlowStr = 'auto';
            }
            $("#" + n.id).css({ bottom: "0px", position: "absolute" }).stop().css({ width: (realWidthTemp) + "px", height: heightBottom + "px", left: leftPosition + left + "px", overflow: overFlowStr });
            //$("#" + n.id).getNiceScroll().resize();
            leftPosition += parseInt(widthTemp);
        });
    };
})(jQuery);

