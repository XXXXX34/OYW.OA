
$(function () {
    $.post("/Welcome/GetCurrentUserInfo", {}, function (data) {
        if (data.Succeed) {
            $("#header-username").html(data.Data.EmplName + "-" + data.Data.DeptName);
            $("#header-username").attr("data-emplid", data.Data.EmplID);
            $("#txtOnlineUserConut").html(data.OnlineUserCount);
            $("#txtNotReadCount").html(data.NotReadCount);
            $("#notifycount").html(data.NotificationCount);
            if (data.VIP == false) {
                $("[class=header]").find("div[class=projectname]").append("<a href='javascript:' onclick='showVIP()' style='text-decoration:none;cursor:pointer;color:red'>(未开通VIP，部分功能无法使用)</a>");
            }
            initSignalR(data.SignalRWeb);
        }
    });
});

function showVIP() {

    $("#dlg_showVIP_globle").remove();
    $("#dlg_all").append("<div class=\"modal fade modal-dialog\" id=\"dlg_showVIP_globle\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\" style=\"z-index:10011\"></div>");
    NProgress.start();
    $("#dlg_showVIP_globle").load("/VIP/Upgrade?t=" + Math.random(), function (data) {
        NProgress.done();
    });
    $("#dlg_showVIP_globle").modal();
}

var lockeScrollLoadData = false;

var chat;


function initSignalR(url) {

    $.connection.hub.url = url + "/signalr";
    chat = $.connection.iMHub;
    chat.client.addMessage = function (msg, relationId, fromEmplName, fromEmpl) {
        try {
            if ($("#dlg_online_globle:visible").length > 0) {
                IMController.addSay(msg, relationId, fromEmplName, fromEmpl, 1);
                chat.server.clearNotRead(IMController.getCurrentRelationID());
            }
        }
        catch (e) {
        }
    };
    chat.client.online = function (count) {
        try {
            $("#txtOnlineUserConut").html(count);
        }
        catch (e) {
        }
    };
    chat.client.refleshMsg = function (str) {

        var lst = $.parseJSON(str);
        var totalCount_IM = 0;
        $(lst).each(function (i, n) {
            totalCount_IM += n.Count;
        });
        $("#txtNotReadCount").html(totalCount_IM);

        if ($("#dlg_online_globle").length > 0) {

            $("#im-relationchats-ul [id^=txt-noreadcount]").each(function (i, n) {
                $(this).html("0");
            });
            var loaded = false;
            $(lst).each(function (i, n) {
                if ($("#txt-noreadcount-" + n.RelationID).length == 0 && n.Count > 0) {
                    if (loaded == false) {
                        IMController.recentRelationList();
                        loaded = true;
                    }
                }
                $("#txt-noreadcount-" + n.RelationID).html(n.Count);
            });

            $("#im-relationchats-ul [id^=txt-noreadcount]").each(function (i, n) {

                if ($(this).html() == "0") {
                    $(this).hide();
                }
                else {
                    if (IMController.getCurrentRelationID() != $(this).parent().parent().attr("data-relation-id"))
                        $(this).show();

                }
            });

        }


    };
    $.connection.hub.start().done(function () {

    });
}


function showOnline() {
    $("body").append($("<div class=\"modal fade modal-dialog modal-lg\" style=\"width:903px\" id=\"dlg_online_globle\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"\"> <div class=\"modal-dialog\" role=\"document\"></div></div>"));
    NProgress.start();
    $("#dlg_online_globle").load("/IM/Chats", function () {
        NProgress.done();
        $("#txtSay").focus();
        $("#btn-im-add-member").click(function () {
            oa.people.getPeople({
                ok: function (r) {


                    //先创建群
                    var members = JSON.stringify(r)

                    //群组
                    if (r.length > 1) {
                        $("#dlg_all").append($("<div style='z-index: 1051;' class=\"modal fade modal-dialog\" id=\"dlg_imgroupname_globle\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"\"> <div class=\"modal-dialog\" role=\"document\"></div></div>"));
                        $("#dlg_imgroupname_globle").load("/IM/GroupName", function () {

                            $("#btn-im-group-name").click(function () {

                                var id = new Date().getSeconds();
                                var name = $("#txt-im-group-name").val();
                                if ($("#tab-" + id).length > 0) return false;

                                var model = {};
                                model.groupName = name;
                                model.members = members;
                                $.post("/IM/SaveGroup?t=" + Math.random(), model, function (data) {

                                    if (data.Succeed) {
                                        IMController.recentRelationList()
                                        $("#dlg_imgroupname_globle").modal("hide");
                                    }
                                    else {
                                        toastr.error(data.Message);//保存失败
                                    }

                                });

                            });
                        });
                        $("#dlg_imgroupname_globle").modal();
                    }
                    else {
                        $(r).each(function (i, n) {
                            if ($("#tab-" + n.emplID).length > 0) return false;

                            var model = {};
                            model.RelationID = n.emplID;
                            model.RelationName = n.emplName;
                            model.RelationType = "Empl";

                            $.post("/IM/SaveRecentRelation/?t=" + Math.random(), model, function (data) {
                                if (data.Succeed) {
                                    IMController.recentRelationList()
                                }
                                else {
                                    toastr.error(data.Message);
                                }
                            });
                        });
                    }
                }
            });
        });
    });
    $("#dlg_online_globle").off("click", "[name=im-relation-item]");
    $("#dlg_online_globle").on("click", "[name=im-relation-item]", function (i, n) {
        var recentid_id = $(this).attr("data-recentid-id");
        current_IM_ID = recentid_id;
        $("#im-member-title").html($(this).attr("data-name"));

        if ($(this).attr("data-relationtype") == "Group") {
            $("#im-member-count").html("(" + $(this).attr("data-count") + ")");
            $("#btn-showMembers").show();
        }
        else {
            $("#btn-showMembers").hide();
        }

        $("#im-chats div[class=im-tab]").hide();
        $("#tab-" + recentid_id).show();
        $(this).siblings().removeClass("selected");
        $(this).addClass("selected");
        $(this).attr("data-page-index", "1");
        chat.server.clearNotRead($(this).attr("data-relation-id"));
        lockeScrollLoadData = true;
        IMController.loadchats('first');

    });
    $("#dlg_online_globle").off("mouseover", "[name=im-relation-item]");
    $("#dlg_online_globle").on("mouseover", "[name=im-relation-item]", function (i, n) {
        $(this).find("[name=im-delete]").addClass("show");
    });
    $("#dlg_online_globle").off("mouseout", "[name=im-relation-item]");
    $("#dlg_online_globle").on("mouseout", "[name=im-relation-item]", function (i, n) {
        $(this).find("[name=im-delete]").removeClass("show");
    });

    $("#dlg_online_globle").off("click", "[name=im-delete]");
    $("#dlg_online_globle").on("click", "[name=im-delete]", function (i, n) {
        var id = $(this).parent().parent().attr("data-recentid-id");
        bootbox.confirm("确认删除?", function (cp) {
            if (cp) {
                IMController.deleteRecent(id);
            }
        })
    });

    $("#dlg_online_globle").modal();
}

function sendIMMessage(e) {
    if (e.key == "Enter") {
        IMController.sendMsg();
    }
}

function seachIMRelation(e) {
    if (e.key == "Enter") {
        $("[name=im-relation-item]").each(function (i, n) {

            var name = $(this).attr("data-name");
            if (!new RegExp($("#txt-im-searchtext").val(), "g").test(name)) {
                $(this).hide();
            }
            else {
                $(this).show();
            }
        })
        if ($("[name=im-relation-item]:visible").length == 0) {
            toastr.warning("未找到配置记录，已自动处理");
            $("#txt-im-searchtext").val("");
            $("[name=im-relation-item]").show();
            $("#im-relationchats-ul li:visible").eq(0).trigger("click");
        }
        else {
            $("#im-relationchats-ul li:visible").eq(0).trigger("click");
        }

    }
}

