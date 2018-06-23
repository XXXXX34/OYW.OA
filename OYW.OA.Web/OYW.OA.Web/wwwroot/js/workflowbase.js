function initWorkFlow() {
    var msgid = $("#hidmessageid").val();
    loadHistory(msgid);
    checkIfCanSubmitOrBack(msgid);
    $("div").delegate("[name=nextnode]", "click", function () {

        if ($(this).is(":checkbox")) {

        }
        else {
            $(this).parent().siblings().find("[name^=handlename]").removeAttr("checked");
        }
    });

    $("#btnPrint").click(function () {
        $(this).parent().hide();
    });


    $("#dvForm table td input").change(function () {
        var id = $(this).attr("id");
        saveEditValue(id, $(this).val());
    });

    $("#dvForm table td select").change(function () {
        var id = $(this).attr("id");
        saveEditValue(id, $(this).val());
    });

    $("#dvForm table td textarea").change(function () {
        var id = $(this).attr("id");
        saveEditValue(id, $(this).val());
    });
}

function saveEditValue(controlid, controlvalue) {
    var msgid = $("#hidmessageid").val();

   
    $.post('/OA.Workflow/FormDesign/SaveEditValue?' + $.param({ messageid: msgid, controlid: controlid, controlvalue: controlvalue }), {}, function (data) {
        var obj = data;
        if (obj.Succeed == true) {
            console.log(obj);
        }
        else {
            alert(data.Message);
        }

    });

}

var curNodeKey_G = "";

function subMitFlow(msgid, targetkey, handleManEcodes) {


    $.post('/OA.Workflow/FormDesign/FlowBussiness?' + $.param({ tag: "subMitFlow", msgid: msgid, curnodekey: curNodeKey_G, targetkey: targetkey, handleManEcodes: handleManEcodes }), {}, function (data) {
        var obj = $.parseJSON(data);
        if (obj.Succeed == "true") {

            window.location.reload();
        }
        else {
            alert(obj.Message);
        }

    });

}


function giveToStart() {

    var msgid = $("#hidmessageid").val();

    if (confirm("确认退回申请人？")) {
        $.post('/OA.Workflow/FormDesign/FlowBussiness?' + $.param({ tag: "giveToStart", msgid: msgid }), {}, function (data) {
            var obj = $.parseJSON(data);
            if (obj.Succeed == "true") {
                window.location.reload();
            }
            else {
                alert(obj.Message);
            }

        });
    }

}

function findNextNode(msgid, tag) {

    $.post('/OA.Workflow/FormDesign/FlowBussiness?' + $.param({ tag: "findNextNode", msgid: msgid }), {}, function (data) {
        var obj = $.parseJSON(data);
        if (obj.Succeed == "true") {


            var curNodeSendModel = obj.SendMode;
            curNodeKey_G = obj.NodeKey;



            var inputType = "";
            var checked = "";
            switch (curNodeSendModel) {
                case "AutoChoice"://自动选择
                    {
                        inputType = "'checkbox'";
                        checked = "checked";
                        break;
                    }
                case "ChoiceOne"://单选
                    {
                        inputType = "'radio'";
                        break;
                    }
                case "ChoiceMutil"://全选
                    {
                        inputType = "'checkbox' checked disabled";
                        checked = "checked";
                        break;
                    }
                default:
                    {
                        inputType = "'radio' ";//disabled
                    }
            }

            $("#" + tag + "_" + msgid).html("");
            var ar = [];
            $.each(obj.Data, function (i, n) {
                ar.push("<div><input " + (checked == "checked" ? "checked" : (i == 0 ? "checked" : "")) + " type=" + inputType + " name='nextnode' data-msgid='" + msgid + "' data-nodekey='" + n.NodeKey + "' /><a style='margin-left:50px' href='javascript:void'>" + n.NodeName + "</a><br/>");

                ar.push("<div style='margin-left:40px;margin-left: 65px; white-space: normal;'>");

                var needCheckedHandleMan = "";

                if ((obj.Data.length == 1 && n.Handlers.length == 1) || (curNodeSendModel == "ChoiceMutil" && n.Handlers.length == 1)) {
                    needCheckedHandleMan = "checked";
                }

                $.each(n.Handlers, function (j, m) {
                    ar.push("<span style='display:inline-block;white-space:nowrap;'><input " + (n.MultiHandleMode == "JustOne" ? ("type='radio' " + needCheckedHandleMan) : (n.MultiHandleMode == "Mutil" ? ("type='checkbox'  " + needCheckedHandleMan) : "type='checkbox' checked  ")) + " name='handlename" + i + "' value='" + m.Ecode + "'/> " + m.Name + "</span>");
                });
                ar.push("</div>");
                ar.push("</div><br/><hr/>");
            });
            $("#nextStep").html(ar.join(""));

            loadHistory(msgid);
        }
        else {
            alert(data.Message);
        }
    });
}

function loadHistory(messageid) {

    $.post('/OA.Workflow/FormDesign/FlowBussiness?' + $.param({ tag: "loadHistory", msgid: messageid }), {}, function (data) {
        var obj = $.parseJSON(data);
        if (obj.Succeed == "true") {
            $("#history li").remove();
            var ar = [];
            $.each(obj.Data, function (i, n) {

                if (i + 1 == obj.Data.length && n.TargetNodeKey != "结束") {
                    ar.push("<li>" + n.NodeKey + "--->" + n.TargetNodeKey + "(" + n.Createtime + ")" + "</li><li>当前处理人：" + n.CurrentHandleName + "</li>");
                }
                else {
                    ar.push("<li>" + n.NodeKey + "--->" + n.TargetNodeKey + "(" + n.Createtime + ")" + "</li>");
                }

                if (i + 1 == obj.Data.length) {
                    if (n.TargetNodeKey == "开始") {
                        $("#btnBackToApp").hide();
                    }
                    if (n.TargetNodeKey == "结束") {
                        $("#btnSubmit").hide();
                        $("#btnBackToApp").hide();
                    }

                }

            });
            $("#history").append(ar.join(""));
        }
        else {
            alert(data.Message);
        }
    });

}

function checkIfCanSubmitOrBack(messageid) {

    $.post('/OA.Workflow/FormDesign/FlowBussiness?' + $.param({ tag: "checkIfCanSubmitOrBack", msgid: messageid }), {}, function (data) {
        var obj = $.parseJSON(data);
        if (obj.Succeed == "true") {
            if (obj.Data.CanGiveBack) {
                $("#btnBackToApp").show();
            }
            if (obj.Data.CanSubmit) {
                $("#btnSubmit").show();
            }
        }
        else {
            alert(data.Message);
        }
    });

}

function submitDlg() {

    var html = [];
    $("#dlg_submit").remove();
    html.push("<div id=\"dlg_submit\" style=\"display: none\">");
    html.push("    <div id=\"nextStep\" style=\"font-weight: bold; font-size: 25px; width: 100%; color: blue; display: inline-block; white-space: nowrap\"></div>");
    html.push("</div>");
    $("body").append(html.join(""));

    var canSubMit = true;
    //检测必填项
    $("input[data-isneed=true]").each(function () {

        if ($.trim($(this).val()) == "") {
            canSubMit = false;
        }
    });
    $("textarea[data-isneed=true]").each(function () {

        if ($.trim($(this).val()) == "") {
            canSubMit = false;
        }
    });

    $("select[data-isneed=true]").each(function () {

        if ($.trim($(this).val()) == "") {
            canSubMit = false;
        }
    });

    if (canSubMit == false) {
        alert("信息填写不完整！");
        return;
    }
    var msgid = $("#hidmessageid").val();

    findNextNode(msgid, "start");


    $("#dlg_submit").dialog({
        width: 530, height: 380, modal: true, resizable: false, title: "下一环节",
        show: { effect: "fade", duration: 300 }, hide: { effect: "blind", duration: 100 }, buttons: {
            '提交': function () {

                if ($("[name=nextnode]:checked").length == 0) {
                    alert("请选择提交的下一环节");
                    return;
                }

                var checkedNextNodes = $("[name=nextnode]:checked").map(function (i, n) { return $(this).data("nodekey"); }).get().join("|");

                var handleManEcodes = "";

                var validateMsg = "";
                $("[name=nextnode]:checked").each(function () {

                    if (handleManEcodes != "") {
                        handleManEcodes += "|";
                    }
                    var checkedMan = $(this).parent().find("[name^=handlename]:checked");
                    if (checkedMan.length == 0 && ($.trim($(this).siblings(":first").html()) != "结束")) {
                        validateMsg = "请选择处理人";
                        return;
                    }
                    handleManEcodes += checkedMan.map(function (i, n) { return $(this).val(); }).get().join(",");
                });
                if (validateMsg != "") {
                    alert(validateMsg);
                    return;
                }

                subMitFlow($("[name=nextnode]:checked").eq(0).data("msgid"), checkedNextNodes, handleManEcodes)
            },
            '关闭': function () { $(this).dialog('close'); }
        }
    });
}


function showHistoryDlg() {

    $("#dlg_showHistory").remove();
    var html = [];
    html.push("");
    html.push("<div id=\"dlg_showHistory\" style=\"display: none\">");
    html.push("    <table style=\"width: 100%\">");
    html.push("        <tr>");
    html.push("            <td style=\"text-align: left\">");
    html.push("                <ul id=\"history\"></ul>");
    html.push("            </td>");
    html.push("        </tr>");
    html.push("    </table>");
    html.push("</div>");

    $("body").append(html.join(""));

    var msgid = $("#hidmessageid").val();
    loadHistory(msgid);
    $("#dlg_showHistory").dialog({
        width: 530, height: 380, modal: true, resizable: false, title: "历史记录",
        show: { effect: "fade", duration: 300 }, hide: { effect: "blind", duration: 100 }, buttons: {
            '关闭': function () { $(this).dialog('close'); }
        }
    });
}


function showFlowImageDlg() {
    $("#dlg_showFlowImage").dialog({
        width: 1200, height: 530, modal: true, resizable: true, title: "流程图",
        show: { effect: "fade", duration: 300 }, hide: { effect: "blind", duration: 100 }, buttons: {
            '关闭': function () { $(this).dialog('close'); }
        }
    });
}