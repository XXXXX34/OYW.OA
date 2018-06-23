
oa = {
    people: {
        beginLoading: function () {
            NProgress.start();
        },
        endLoading: function () {
            NProgress.done();
        },
        getFileHtml: function () {
            return "<div class=\"modal fade modal-dialog modal-lg\" id=\"dlg_filemgr_globle\" tabindex=\"1\" role=\"dialog\" aria-hidden=\"true\" style=\"z-index:10011\"></div>";
        },
        uploadFile: function (option) {
            var me = this;
            files_g = [];
            $("#dlg_filemgr_globle").remove();
            $("#dlg_all").append(me.getFileHtml());
            me.beginLoading();
            $("#dlg_filemgr_globle").load("/FileMgr/Index?t=" + Math.random(), function (data) {
                me.endLoading();
            });
            $("#oa-file-ul li").remove();

            $("#dlg_filemgr_globle").modal();
            $("#dlg_filemgr_globle").draggable({
                handle: ".modal-header",
                cursor: 'move',
                refreshPositions: false
            });

            $("#dlg_filemgr_globle").off("click", "#btn-oa-file-globle-ok");
            $("#dlg_filemgr_globle").on("click", "#btn-oa-file-globle-ok", function (i, n) {
                if ($("#dlg_filemgr_globle div[class*=state-ready]").length > 0) {
                    toastr.warning("请先点击上传，再保存");
                    return;
                }
                option.ok(files_g);
                $("#dlg_filemgr_globle").modal("hide");
            });
        },
        getRoleHtml: function () {
            return "    <div class=\"modal fade modal-dialog\" id=\"dlg_role_globle\" tabindex=\"-1\" style=\"z-index:1051\" role=\"dialog\" aria-hidden=\"true\">      <div class=\"modal-dialog\" role=\"document\">          <div class=\"modal-content\">              <div class=\"modal-header\">                  <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">                      <span aria-hidden=\"true\">×</span>                  </button>                  <h4 class=\"modal-title\">选择角色</h4>              </div>              <div class=\"modal-body\" id=\"oa-role-globle-searchField\">                  <table id=\"oa-table-normal-globle-role\" class=\"table table-striped table-hover\">                      <thead>                          <tr class=\"borderleft\">                              <td style=\"width:40px\"><input type=\"checkbox\" disabled /></td>                              <td>角色名称</td>                              <td>角色描述</td>                          </tr>                      </thead>                      <tbody></tbody>                  </table>              </div>              <div class=\"modal-footer\">                  <button type=\"button\" id=\"btn-oa-role-globle-ok\" class=\"btn btn-primary\"><span class=\"glyphicon glyphicon-floppy-disk\" aria-hidden=\"false\"></span>确定</button>                  <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>关闭</button>              </div>          </div>      </div>  </div>";
        },
        getRole: function (option) {
            $("#dlg_role_globle").remove();
            $("#dlg_all").append(this.getRoleHtml());

            $("#dlg_role_globle").modal();

            this.loadRoleList();

            $("#dlg_role_globle").off("click", "#btn-oa-role-globle-ok");
            $("#dlg_role_globle").on("click", "#btn-oa-role-globle-ok", function (i, n) {
                var res = [];
                $($("#dlg_role_globle input[name=chk_column_role]:checked")).each(function (j, k) {
                    res.push({ roleID: $(this).attr("data-id"), roleName: $(this).attr("data-name") });
                });
                if (res.length == 0) res.push({ roleID: "", roleName: "" });
                option.ok(res);
                $("#dlg_role_globle").modal("hide");
            });
        },
        loadRoleList: function () {
            var me = this;
            var obj = $.extend({}, {});
            obj.pageIndex = 1;
            obj.pageSize = 100;
            obj.orderField = "";
            obj.ascending = false;
            obj.t = Math.random();
            me.beginLoading();
            $.post('/OA.People/Role/RoleList/?' + jQuery.param(obj), function (data) {
                if (data.Succeed == true) {
                    me.endLoading();
                    $('#oa-table-normal-globle-role tbody tr').remove();
                    $.each(data.Data, function (i, n) {
                        me.addRoleRow(n);
                    });
                }
                else {
                    toastr.error(data.Message); //保存失败
                }

            }, 'json');
        },
        addRoleRow: function (row) {
            $('#oa-table-normal-globle-role tbody').append($('<tr>' +
            '<td><input type="checkbox" value="' + row.RoleID + '" name="chk_column_role" data-id="' + row.RoleID + '" data-name="' + row.RoleName + '"/></td>' +
            '<td>' + row.RoleName + '</td>' +
            '<td>' + row.RoleDescr + '</td>' +
            '</tr>'));
        },

        getPeopleHtml: function () {
            return "    <div class=\"modal fade modal-dialog modal-lg\" id=\"dlg_people_globle\" tabindex=\"-1\" style=\"z-index:1051\" role=\"dialog\" aria-hidden=\"true\">      <div class=\"modal-dialog\" role=\"document\">          <div class=\"modal-content\">              <div class=\"modal-header\">                  <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">                      <span aria-hidden=\"true\">×</span>                  </button>                  <h4 class=\"modal-title\">选择人员</h4>                  <div class=\"btn-group\" style=\"float:right;margin-right:20px\">                      <div class=\"form-inline\">                          <div class=\"form-group\">                              <div class=\"input-group\">                                  <div class=\"input-group-addon\">姓名</div>                                  <input type=\"text\" class=\"form-control input-sm\" id=\"txtEmplName_G\" placeholder=\"回车键查询\">                              </div>                          </div>                      </div>                  </div>              </div>              <div class=\"modal-body\" id=\"oa-people-globle-searchField\">                  <div style=\"float:left;width:200px;border-right:1px solid gray;height:300px;overflow:auto\">                      <div id=\"people_globle_tree\"></div>                  </div>                  <div style=\"float:left;height:400px;overflow:auto;width:340px;margin-left:10px\">                      <table id=\"oa-table-normal-globle-people\" class=\"table table-striped table-hover\">                          <thead>                              <tr class=\"borderleft\">                                  <td style=\"width:40px\"><input type=\"checkbox\" id=\"chk_choice_people_glb\"  /></td>                                  <td></td>                              </tr>                          </thead>                          <tbody></tbody>                      </table>                  </div>              </div>              <div style=\"clear:both\"></div>              <div class=\"modal-footer\">                  <button type=\"button\" id=\"btn-oa-people-globle-ok\" class=\"btn btn-primary\"><span class=\"glyphicon glyphicon-floppy-disk\" aria-hidden=\"false\"></span>确定</button>                  <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>关闭</button>              </div>          </div>      </div>  </div>";
        },
        getPeople: function (option) {
            $("#dlg_people_globle").remove();
            $("#dlg_all").append(this.getPeopleHtml());
            $("#chk_choice_people_glb").click(function () {
                var checked = $("#chk_choice_people_glb:checked").length;
                if (checked) {
                    $("input[name='chk_column_peop']").attr("checked", 'true');
                }
                else {
                    $("input[name='chk_column_peop']").removeAttr("checked");
                }
            });
            $("#dlg_people_globle").modal();
            var me = this;
            $("#people_globle_tree").dynatree({
                minExpandLevel: 2,
                clickFolderMode: 3,
                initAjax: {
                    url: "/OA.People/Home/LoadDept?t=" + Math.random(),
                    data: { node: '0', type: 'readonly' }
                },
                onPostInit: function (isReloading, isError) {
                    this.reactivate();
                    var tree = $("#people_globle_tree").dynatree("getTree");
                    var nod = tree.getNodeByKey("fe6258ca-1ade-4a62-ae9f-dca5d7715da0");
                    nod.toggleExpand();
                    tree.activateKey("fe6258ca-1ade-4a62-ae9f-dca5d7715da0")
                    me.loadEmployeeList(nod.data.key);
                },
                onActivate: function (node) {
                    try {

                    } catch (mg) {
                        return;
                    }
                },
                onLazyRead: function (node) {
                    node.appendAjax({
                        url: "/OA.People/Home/LoadDept",
                        data: { node: node.data.key, type: 'readonly' },
                        success: function (node) {
                        }
                    });
                },
                onClick: function (node, event) {
                    me.loadEmployeeList(node.data.key);
                }
            });

            $("#dlg_people_globle").off("keydown", "#txtEmplName_G");
            $("#dlg_people_globle").on("keydown", "#txtEmplName_G", function (event) {
                if (event.keyCode == 13) {
                    var tree = $("#people_globle_tree").dynatree("getTree");
                    var node = tree.getActiveNode();
                    me.loadEmployeeList(node.data.key);
                }
            });


            $("#dlg_people_globle").off("click", "#btn-oa-people-globle-ok");
            $("#dlg_people_globle").on("click", "#btn-oa-people-globle-ok", function (i, n) {

                var res = [];
                $($("#dlg_people_globle [name=chk_column_peop]:checked")).each(function (j, k) {
                    res.push({ emplID: $(this).attr("data-id"), emplName: $(this).attr("data-name") });
                });
                if (res.length == 0) res.push({ emplID: "", emplName: "" });
                option.ok(res);
                $("#dlg_people_globle").modal("hide");
            });
        },
        loadEmployeeList: function (deptid) {
            var me = this;
            $.post('/OA.People/Home/EmployeeList/' + deptid + '?t=' + Math.random(), { emplName: $("#txtEmplName_G").val() }, function (data) {
                if (data.Succeed == true) {
                    me.endLoading();
                    var html = [];
                    $("#oa-table-normal-globle-people tbody tr").remove();
                    $(data.Data).each(function (i, row) {
                        $('#oa-table-normal-globle-people tbody').append($('<tr>' +
                        '<td><input type="checkbox" value="' + row.EmplID + '" name="chk_column_peop" data-id="' + row.EmplID + '" data-name="' + row.EmplName + '"/></td>' +
                        '<td><img style=\"width:26px;height:26px\" src=\"/IM/ShowImplHeader/' + row.EmplID + '\" /><span style="margin-left:20px;font-size:14px">' + row.EmplName + '</span></td>' +
                        '</tr>'));
                    });

                    me.endLoading();

                }
                else {
                    toastr.error(data.Message); //保存失败
                }
            }, 'json');


        },
        getPosHtml: function () {

            return "<div class=\"modal fade modal-dialog\" id=\"dlg_pos_globle\" tabindex=\"-1\" style=\"z-index:1051\" role=\"dialog\" aria-hidden=\"true\">      <div class=\"modal-dialog\" role=\"document\">          <div class=\"modal-content\">              <div class=\"modal-header\">                  <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">                      <span aria-hidden=\"true\">×</span>                  </button>                  <h4 class=\"modal-title\">选择职位</h4>              </div>              <div class=\"modal-body\" id=\"oa-pos-globle-searchField\">                  <table id=\"oa-table-normal-globle\" class=\"table table-striped table-hover\">                      <thead>                          <tr class=\"borderleft\">                              <td style=\"width:40px\"><input type=\"checkbox\" disabled /></td>                              <td>职位名称</td>                              <td>职位描述</td>                          </tr>                      </thead>                      <tbody></tbody>                  </table>              </div>              <div class=\"modal-footer\">                  <button type=\"button\" id=\"btn-oa-pos-globle-ok\" class=\"btn btn-primary\"><span class=\"glyphicon glyphicon-floppy-disk\" aria-hidden=\"false\"></span>确定</button>                  <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>关闭</button>              </div>          </div>      </div>  </div>"
        },
        getPos: function (option) {
            $("#dlg_pos_globle").remove();
            $("#dlg_all").append(this.getPosHtml());
            $("#dlg_pos_globle").modal();

            this.loadPosList();

            $("#dlg_pos_globle").off("click", "#btn-oa-pos-globle-ok");
            $("#dlg_pos_globle").on("click", "#btn-oa-pos-globle-ok", function (i, n) {
                var res = [];
                $($("#dlg_pos_globle input[name=chk_column_pos]:checked")).each(function (j, k) {
                    res.push({ posID: $(this).attr("data-id"), posName: $(this).attr("data-name") });
                });
                if (res.length == 0) res.push({ posID: "", posName: "" });
                option.ok(res);
                $("#dlg_pos_globle").modal("hide");
            });
        },
        loadPosList: function () {
            var me = this;
            var obj = {};
            obj.pageIndex = 1;
            obj.pageSize = 500;
            obj.orderField = "";
            obj.ascending = false;
            obj.t = Math.random();
            me.beginLoading();
            $.post('/OA.People/Position/PositionList/?' + jQuery.param(obj), function (data) {
                if (data.Succeed == true) {
                    me.endLoading();
                    $('#oa-table-normal-globle tbody tr').remove();
                    $.each(data.Data, function (i, n) {
                        me.addPositionRow(n);
                    });
                }
                else {
                    alert(data.Message);
                }
            }, 'json');
        },
        addPositionRow: function (row) {
            $('#oa-table-normal-globle tbody').append($('<tr>' +
           '<td><input type="checkbox" value="' + row.PositionID + '" name="chk_column_pos" data-id="' + row.PositionID + '" data-name="' + row.PositionName + '"/></td>' +
           '<td>' + row.PositionName + '</td>' +
           '<td>' + row.PositionDescr + '</td>' +
           '</tr>'));
        },
        getDeptHtml: function () {
            var html = "  <div class=\"modal fade modal-dialog\" id=\"dlg_dept_globle\" tabindex=\"-1\" style=\"z-index:1051\" role=\"dialog\" aria-hidden=\"true\">      <div class=\"modal-dialog\" role=\"document\">            <div class=\"modal-content\">              <div class=\"modal-header\">                  <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button>                  <h4 class=\"modal-title\">选择部门</h4>              </div>              <div class=\"modal-body\" id=\"oa-dept-globle-searchField\">                  <div id=\"oa-dept-globle-tree\"></div>              </div>              <div class=\"modal-footer\">                  <button type=\"button\" id=\"btn-oa-dept-globle-ok\" class=\"btn btn-primary\"><span class=\"glyphicon glyphicon-floppy-disk\" aria-hidden=\"false\"></span>确定</button>                  <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>关闭</button>              </div>          </div>        </div>  </div>";
            return html;
        },
        getDept: function (option) {
            $("#dlg_dept_globle").remove();
            $("#dlg_all").append(this.getDeptHtml());

            $("#dlg_dept_globle").modal();

            var oa_dept_globle_node = [];
            $("#oa-dept-globle-tree").dynatree({
                minExpandLevel: 2,
                checkbox: true,
                selectMode: option.mutil ? 2 : 1, // 1:single, 2:multi, 3:multi-hier
                clickFolderMode: 3,
                initAjax: {
                    url: "/OA.People/Home/LoadDept?t=" + Math.random(),
                    data: { node: '0', type: 'readonly' }
                },
                onPostInit: function (isReloading, isError) {
                    this.reactivate();
                    var tree = $("#oa-dept-globle-tree").dynatree("getTree");
                    var nod = tree.getNodeByKey("fe6258ca-1ade-4a62-ae9f-dca5d7715da0");
                    nod.toggleExpand();
                    tree.activateKey("fe6258ca-1ade-4a62-ae9f-dca5d7715da0")

                },
                onActivate: function (node) {
                    try {

                    } catch (mg) {
                        return;
                    }
                },
                onLazyRead: function (node) {
                    node.appendAjax({
                        url: "/OA.People/Home/LoadDept",
                        data: { node: node.data.key, type: 'readonly' },
                        success: function (node) {
                        }
                    });
                },
                onClick: function (node, event) {

                }
            });

            $("#dlg_dept_globle").off("click", "#btn-oa-dept-globle-ok");
            $("#dlg_dept_globle").on("click", "#btn-oa-dept-globle-ok", function (i, n) {
                var tree = $("#oa-dept-globle-tree").dynatree("getTree");
                oa_dept_globle_node = tree.getSelectedNodes();
                var res = [];
                $(oa_dept_globle_node).each(function (j, k) {
                    res.push({ deptID: k.data.key, deptName: k.data.title });
                });
                if (res.length == 0) res.push({ deptID: "", deptName: "" });
                option.ok(res);
                $("#dlg_dept_globle").modal("hide");
            });
        }
    }
};


var securityType_glb_glb = "";
var permissionCode_glb_glb = "";
var appCode_glb_glb = "";


var permissionSetGlb = {
    beginLoading: function () {
        NProgress.start();
    },
    endLoading: function () {
        NProgress.done();
    },
    addPermission: function (r) {
        switch (securityType_glb_glb) {
            case "user":
                {
                    $(r).each(function (i, n) {
                        permissionSetGlb.addPermissionMethod(permissionCode_glb_glb, appCode_glb_glb, n.emplID, 'user');
                    });
                    break;
                }
            case "dept":
                {
                    $(r).each(function (i, n) {
                        permissionSetGlb.addPermissionMethod(permissionCode_glb_glb, appCode_glb_glb, n.deptID, 'dept');
                    });
                    break;
                }
            case "role":
                {
                    $(r).each(function (i, n) {
                        permissionSetGlb.addPermissionMethod(permissionCode_glb_glb, appCode_glb_glb, n.roleID, 'role');
                    });
                    break;
                }
        }
    },
    addPermissionMethod: function (permissionCode, objectID, securityID, securityType) {
        var obj = { PermissionCode: permissionCode, ObjectID: objectID, SecurityID: securityID, SecurityType: securityType };
        permissionSetGlb.beginLoading();
        $.post('/PermissionSet/SavePermission/?t=' + Math.random(), obj, function (data) {
            if (data.Succeed == true) {
                permissionSetGlb.endLoading();
            }
            permissionSetGlb.loadSetPermissionList();
        });
    },

    loadSetPermissionList: function () {
        $.post('/PermissionSet/SetPermissionList/?t=' + Math.random(), { permissionCode: permissionCode_glb_glb }, function (data) {
            if (data.Succeed == true) {
                $("#tb_permission_user tbody tr").remove();
                $("#tb_permission_dept tbody tr").remove();
                $("#tb_permission_role tbody tr").remove();
                $(data.Data).each(function (i, n) {
                    var html = [];
                    html.push("                            <tr>");
                    html.push("                                <td>" + n.SecurityName + "</td>");
                    html.push("                                <td style='padding-left: 10px;'>");
                    html.push("                                    <input type=\"checkbox\" name=\"permissionitem\" data-type=\"" + n.PermissionCode + "\" data-id=\"" + n.PermissionID + "\" />");
                    html.push("                                </td>");
                    html.push("                            </tr>");
                    $("#tb_permission_" + n.SecurityType + " tbody").append(html.join(" "));
                });
            }
        });
    },
    deleteSetPermission: function () {    //删除
        var me = this;
        var idList = "";
        $("#permissionObjectCodeForm [name=permissionitem]:checked").each(function (i, n) {
            if (idList != "")
                idList += ",";
            idList += $(this).attr("data-id");
        });

        if (idList == "") {
            toastr.warning('请选择要删除的记录!');
            return;
        };
        bootbox.confirm("确认删除?", function (cp) {
            if (cp) {
                $.post('/PermissionSet/DeletePermission/?idlist=' + idList + '&t=' + Math.random(), function (data) {
                    if (data.Succeed == true) {
                        toastr.success("删除成功");//提示
                        me.loadSetPermissionList();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                }, 'json');
            }
        })

    },
    setPermissionObjectCode: function (id, permissionCode, appCode, title) {//授权
        permissionCode_glb_glb = permissionCode;
        appCode_glb_glb = appCode;
        var me = this;
        var ID = id;
        $("#dlg_setpermissionObjectCode_glb").remove(); //保证不会因为缓存影响数据modal-lg modal-full
        $("body").append($("<div class=\"modal fade modal-dialog modal-lg\" id=\"dlg_setpermissionObjectCode_glb\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\"> <div class=\"modal-dialog\" role=\"document\"></div></div>"));
        me.loadSetPermissionObjectCodeContent(ID, title);
        $("#dlg_setpermissionObjectCode_glb").modal();
    },
    loadSetPermissionObjectCodeContent: function (id, title) {
        var me = this;
        me.beginLoading();
        $("#dlg_setpermissionObjectCode_glb").load('/PermissionSet/SetPermissionObjectCode/' + id + '?t=' + Math.random(), function () {
            permissionSetGlb.loadSetPermissionList();
            $("#permissionObjectCodeModalTitle").html(title);
            me.endLoading();
        });
    }
};

var people_my = {};
function showPeopleinfo() {
    $("#dlg_peopleinfo_globle").remove();
    $("body").append('<div class="modal fade modal-dialog" id="dlg_peopleinfo_globle" tabindex="-1" role="dialog" aria-hidden="true"></div>');
    $("#dlg_peopleinfo_globle").modal();
    $("#dlg_peopleinfo_globle").load("/People/PeopleInfo", function (data) {
        NProgress.start();
        $.post('/People/EmployeeDetails/?t=' + Math.random(), {}, function (data) {
            NProgress.done();
            if (data.Succeed == true) {
                way.registerBindings();
                people_my = data.Data;
                formatModel(people_my);
                people_my.EmplBirth = timeStamp2StringShort(people_my.EmplBirth);
                people_my.AttendDate = timeStamp2StringShort(people_my.AttendDate);
                way.set("peopleInfoScope", people_my);
                $("#imgHeader").attr("src", "/OA.Doc/Home/DownloadFile/" + people_my.HeaderImage);
            }
            else {
                toastr.error(data.Message); //保存失败
            }
        }, 'json');

    });
}
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

var oaController = {
    sortPage: function (tbID, field, sortControl) {
        var currentSortField = $("#" + sortControl).prop("data-field");
        var boolAscending = $("#" + sortControl).prop("data-ascending");
        if (currentSortField == field) {
            if (boolAscending == "true") {
                $("#" + sortControl).prop("data-ascending", "false")
            }
            else {
                $("#" + sortControl).prop("data-ascending", "true")
            }
        }
        else {
            $("#" + sortControl).prop("data-ascending", "false")
        }
        $("#" + sortControl).prop("data-field", field);
        //设置升序、降序图标
        var arImg = ["/images/arrow-up.png", "/images/arrow-down.png"];
        var imgSort = "";
        if ($("#" + sortControl).prop("data-ascending") == "true") {
            imgSort = "<img class=\"sort\" src=\"" + arImg[1] + "\" />";
        }
        else {
            imgSort = "<img class=\"sort\" src=\"" + arImg[0] + "\" />";
        }

        $("#" + tbID + "  thead tr td img[class=sort]").remove();
        $("#" + tbID + "  thead tr td a[href*='" + $("#" + sortControl).prop("data-field") + "']").append(imgSort);

    },
    lightRow: function () {

        $(".oa-table tbody tr ").mouseenter(function () {
            $(this).addClass("hover");
        }).mouseleave(function () {
            $(this).removeClass("hover");
        });
    },
    setlayout: function () {
        var topRow = [];
        var middleRow = [{ id: "oa-left", width: "180", height: "fill" }, { id: "splitBar", width: "10", height: "fill" }, { id: "oa-right", width: "fill", height: "fill" }];
        var bottomRow = [];
        $.layoutMY(topRow, middleRow, bottomRow, window, 40, 85);
        var topRow1 = [{ id: "oa-searchBar", width: "fill", height: "auto" }];
        var middleRow2 = [{ id: "oa-list", width: "fill", height: "fill" }];
        var bottomRow3 = [];
        $.layoutMY(topRow1, middleRow2, bottomRow3, $("#oa-right"), 0, 0, 3, 2);
        $("#oa-right").css({ overflow: 'hidden' });
    },
    toggleDeleteBtn: function (btnDeleteCtr, tableNormalCtr) {
        if ($("#" + tableNormalCtr + " tbody").find("input:checked").length > 0) {
            $("#" + btnDeleteCtr).show();
        }
        else {
            $("#" + btnDeleteCtr).hide();
        }
    },
    bindEvents: function (btnSearchID, checkAllCtr, chk_columnCtr, searchFieldCtr, seniorSearchFieldCtr, btnDeleteCtr, tableNormalCtr) {
        if (!checkAllCtr) checkAllCtr = "chk_all";
        if (!chk_columnCtr) chk_columnCtr = "chk_column";
        if (!searchFieldCtr) searchFieldCtr = "oa-searchField";
        if (!seniorSearchFieldCtr) seniorSearchFieldCtr = "oa-senior-searchField";
        if (!btnDeleteCtr) btnDeleteCtr = "btnDelete";
        if (!tableNormalCtr) tableNormalCtr = "oa-table-normal";

        $("#" + checkAllCtr).unbind("click").click(function () {
            var checked = $(this).is(':checked');
            $("input[type=checkbox][name=" + chk_columnCtr + "]").prop("checked", checked);
            if (checked == true) {
                $(this).parent().parent().parent().parent().find("tbody tr").addClass("select");
            }
            else {
                $(this).parent().parent().parent().parent().find("tbody tr").removeClass("select");
            }
            oaController.toggleDeleteBtn(btnDeleteCtr, tableNormalCtr);
        });
        $("input[name=" + chk_columnCtr + "]").unbind("click").click(function () {
            oaController.toggleDeleteBtn(btnDeleteCtr, tableNormalCtr);
            var checked = $(this).is(':checked');
            var $parent = $(this).parent().parent();
            if (checked == true) {
                $($parent).addClass("select");
            }
            else {
                $($parent).removeClass("select");
            }
        });
        $("input[name=" + chk_columnCtr + "]").parent().parent().find("td:first").siblings().unbind("click").click(function () {
            if ($(this).find("a").length > 0) {
                return;
            }
            var $parent = $(this).parent()
            var checked = $($parent).find("input[name=" + chk_columnCtr + "]").is(':checked');
            $($parent).find("input[name=" + chk_columnCtr + "]").prop("checked", !checked);
            oaController.toggleDeleteBtn(btnDeleteCtr, tableNormalCtr);
            if (!checked == true) {
                $($parent).addClass("select");
            }
            else {
                $($parent).removeClass("select");
            }
        });

        if (btnSearchID != undefined && btnSearchID != null && btnSearchID != "") {
            $("#" + searchFieldCtr + " span select,#" + searchFieldCtr + "  select").each(function () {
                $(this).unbind("change").change(function () {
                    $("#" + btnSearchID).trigger("click");
                });
            });
            $("#" + searchFieldCtr + " span input,#" + searchFieldCtr + "  input").each(function () {
                $(this).unbind("keydown").keydown(function (e) {
                    if (e.keyCode == 13) {
                        $("#" + btnSearchID).trigger("click");
                    }
                });
            });
        }

    },
    fixHelper: function (e, ui) {
        ui.children().each(function () {
            $(this).width($(this).width());
        });
        return ui;
    },
    sortTable: function (id) {
        var me = this;
        $("#" + id + " tbody").sortable({
            helper: me.fixHelper,
            axis: "y",
            start: function (e, ui) {
                ui.helper.addClass('ui-state-highlight')
                return ui;
            },
            stop: function (e, ui) {
                ui.item.removeClass("ui-state-highlight");
                //projTaskController.saveSort();
                return ui;
            }
        }).disableSelection();
    },
}

$(function () {

    initCircleMenu();

    NProgress.configure({ minimum: 0.7 });

    loadMenus();
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

function showNotify() {
    $("#dlg_notify_globle").remove();
    $("body").append("<div class=\"modal fade modal-dialog\" id=\"dlg_notify_globle\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\">          <div class=\"modal-dialog\" role=\"document\">              <div class=\"modal-content\">                  <div class=\"modal-header\" style=\"background-color:#eaedf1;padding-right: 20px;\">                      <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button>                      <h4 class=\"modal-title\">消息通知</h4>                  </div>                  <div class=\"modal-body\" id=\"oa-notify-searchField\" style=\"max-height:300px !important;\">                      <div id=\"notify-list\" class=\"notify-list\">                      </div>                  </div>                  <div class=\"modal-footer\" style=\"padding-right:20px;\">                      <div class=\"form-inline\">             <input type='checkbox'  onclick='chkAllNotify(this)' style='float:left;margin:0 10px 0 20px;height:30px' />                <select id=\"txt-oa-notify-notificationType\" class=\"form-control input-sm\" onchange=\"notificationController.search();\" style=\"float:left\">                              <option value=\"\">全部</option>  <option value=\"日程安排\">日程安排</option><option value=\"会议安排\">会议安排</option><option value=\"客户回访\">客户回访</option> <option value=\"文件分享\">文件分享</option> <option value=\"任务管理\">任务管理</option>    <option value=\"其它\">其它</option>                       </select>                          <input id=\"txt-oa-notify-text\" class=\"form-control input-sm\" placeholder=\"回车键查询\" style=\"float:left\" onkeydown=\"notifySearch(event)\" />                       <button type=\"button\" class=\"btn btn-primary\" onclick=\"notificationController.readNotification();\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>已读</button>                          <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span>关闭</button>                      </div>                  </div>              </div>          </div>      </div> ");
    $("#dlg_notify_globle").modal();
    notificationController.search();
};

function chkAllNotify(sender) {
    if ($(sender).is(":checked")) {
        $("#dlg_notify_globle input[name=notify-item-chk]").attr("checked", true);
    }
    else {
        $("#dlg_notify_globle input[name=notify-item-chk]").removeAttr("checked");
    }
}

function notifySearch(e) {
    if (e.key == "Enter") {
        notificationController.search();
    }
};

var notification = {};
var notificationController = {
    beginLoading: function () {
        NProgress.start();
    },
    endLoading: function () {
        NProgress.done();
    },
    readNotification: function () {
        var me = this;
        var idList = me.getCheckedRecord();
        if (idList == "") {
            toastr.warning('请选择记录!');
            return;
        };
        $.post('/Notification/ReadNotification/?idlist=' + idList + '&t=' + Math.random(), function (data) {
            if (data.Succeed == true) {
                me.search();
            }
            else {
                toastr.error(data.Message);
            }
        }, 'json');
    },

    loadList: function (objCondition) {
        var me = this;
        me.apendList(objCondition);
    },
    apendList: function (objCondition) {
        var me = this;
        var obj = $.extend({}, objCondition);
        obj.pageIndex = 1;
        obj.pageSize = 2000;
        obj.orderField = $("#hidSortField").attr("data-field");
        obj.ascending = $("#hidSortField").attr("data-ascending");
        obj.t = Math.random();
        me.beginLoading();
        $.post('/Notification/NotificationList/?' + jQuery.param(obj), function (data) {
            if (data.Succeed == true) {
                $("#notifycount").html(data.TotalCount);
                me.endLoading();
                if ($('#notify-list').length == 0) return;
                $('#notify-list').html("");
                var html = [];

                $.each(data.Data, function (i, n) {
                    html.push("<div class=\"notify-item\"> ");
                    html.push("    <input  name=\"notify-item-chk\" value=\"" + n.NotificationID + "\" type=\"checkbox\" /> ");
                    html.push("    <a title=\"" + n.Text + "\" href=\"" + n.URL + "\" target=\"_blank\"> ");
                    html.push(n.Text);
                    html.push("    </a> ");
                    html.push("    <span class=\"notify-creatime\">" + timeStamp2String(n.CreateTime) + "</span> ");
                    html.push("</div> ");
                });
                $('#notify-list').html(html.join(" "));
            }
            else {
                toastr.error(data.Message);
            }
            me.bindEvents();
        }, 'json');
    },

    getCheckedRecord: function () {
        var me = this;
        var idList = '';
        idList = $.map($("#notify-list").find("[name=notify-item-chk]:checked"), function (n) {
            if (n.checked == true) {
                var v = $(n).val();
                return v;
            }
        }).join(","); return idList;
    },
    search: function () {//查询
        var me = this; currentPage = 1;
        var objCondition = me.getCondition();
        me.loadList(objCondition);
    },

    getCondition: function () {
        var objCondition = {};
        objCondition.notificationType = $("#txt-oa-notify-notificationType").val();
        objCondition.text = $("#txt-oa-notify-text").val();
        return objCondition;
    },
    clearSearch: function () {//清空查询
        var me = this;

    },
    sort: function (field) {
        var me = this;
    },
    bindEvents: function () {
        var me = this;
    }
};

function initCircleMenu() {
    var htmlMenu = [];
    htmlMenu.push("    <div id='oa_circle_menu_cnt' class=\"htmleaf-container\">");
    htmlMenu.push("        <div id='oa_circle_menu'>");
    htmlMenu.push("            <div name='button' onclick=\"showFastDoc()\">");
    htmlMenu.push("                <i class=\"fa\"><span class=\"glyphicon glyphicon-file\"></span></i>");
    htmlMenu.push("            </div>");
    htmlMenu.push("            <div name='button' onclick=\"showFastCal()\">");
    htmlMenu.push("                <i class=\"fa\"><span class=\"glyphicon glyphicon-calendar\"></span></i>");
    htmlMenu.push("            </div>");
    htmlMenu.push("            <div name='button' onclick=\"showOnline()\">");
    htmlMenu.push("                <i class=\"fa\"><span class=\"glyphicon glyphicon-comment\"></span></i>");
    htmlMenu.push("            </div>");
    htmlMenu.push("            <div name='button' onclick=\"showNotify()\">");
    htmlMenu.push("                <i class=\"fa\"><span class=\"glyphicon glyphicon-bell\"></span></i>");
    htmlMenu.push("            </div>");
    htmlMenu.push("            <div class='menu'>");
    htmlMenu.push("                <div class='share' id='ss_toggle' data-rot='180'>");
    htmlMenu.push("                    <div class='circle'></div>");
    htmlMenu.push("                    <div class='bar'></div>");
    htmlMenu.push("                </div>");
    htmlMenu.push("            </div>");
    htmlMenu.push("        </div>");
    htmlMenu.push("    </div>");
    $("body").append(htmlMenu.join(" "));
    $("#oa_circle_menu").draggable();

    var toggle = $('#ss_toggle');
    var menu = $('#oa_circle_menu');
    var rot;
    $('#ss_toggle').on('click', function (ev) {
        rot = parseInt($(this).data('rot')) - 180;
        menu.css('transform', 'rotate(' + rot + 'deg)');
        menu.css('webkitTransform', 'rotate(' + rot + 'deg)');
        if (rot / 180 % 2 == 0) {
            toggle.parent().addClass('ss_active');
            toggle.addClass('close');
            $("#oa_circle_menu div[name=button]").show();
        } else {
            toggle.parent().removeClass('ss_active');
            toggle.removeClass('close');
            $("#oa_circle_menu div[name=button]").hide();
        }
        $(this).data('rot', rot);
    });
    menu.on('transitionend webkitTransitionEnd oTransitionEnd', function () {
        if (rot / 180 % 2 == 0) {
            $('#ss_menu div i').addClass('ss_animate');
        } else {
            $('#ss_menu div i').removeClass('ss_animate');
        }
    });
}

function showFastCal() {
    $("#dlg_fastcal_schedule").remove(); //保证不会因为缓存影响数据modal-lg modal-full
    $("body").append($("<div class=\"modal fade modal-dialog\" id=\"dlg_fastcal_schedule\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\"> <div class=\"modal-dialog\" role=\"document\"></div></div>"));
    $("#dlg_fastcal_schedule").load("/OA.Calendar/Wiget/FastCal", {}, function () { });
    $("#dlg_fastcal_schedule").modal();
}
function showFastDoc() {
    $("#dlg_fastdoc_schedule").remove(); //保证不会因为缓存影响数据modal-lg modal-full
    $("body").append($("<div class=\"modal fade modal-dialog\" id=\"dlg_fastdoc_schedule\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" style=\"z-index: 1044;\"> <div class=\"modal-dialog\" role=\"document\"></div></div>"));
    $("#dlg_fastdoc_schedule").load("/OA.Doc/Wiget/FastDoc", {}, function () { });
    $("#dlg_fastdoc_schedule").modal();
}
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
    $.post("/Menu/GetMenus", {}, function (data) {
        if (data.Succeed) {
            var html = [];
            $(data.Data).each(function (i, n) {
                html.push('<li title="' + n.MenuName + '" style="position:relative">');//n.URL
                html.push('<a href="' + n.URL + '" target="_self" name="' + n.Code + '"  onclick="window.open(\'' + n.URL + '\', \'_self\')"><span class="' + n.Icon + '" aria-hidden="true"></span></a>');
                html.push('</li>');
                menu_G[n.Code] = n.SubMenus;
                menu_G[n.Code + "_M"] = n;
            });
            $("#oa-app ul li").remove();
            $("#oa-app ul").append(html.join(" "));

            //绘制二级菜单
            var app = $.getUrlParam("app");
            var subMenus = menu_G[app];
            html = [];
            $(subMenus).each(function (j, k) {
                html.push(' <li>');
                html.push('<div name="' + k.Code + '" class="item">');
                html.push(' <a href="' + k.URL + '">' + k.MenuName + '</a>');
                html.push('</div>');
                html.push('</li>');

            });
            $("#oa-submenus ul li").remove();
            $("#oa-submenus ul").append(html.join(" "));

            //选中菜单
            selectedMenu();

            $("#oa-left-title").html(menu_G[app + "_M"].MenuName);
        }
        else {
            toastr.error(data.Message);//保存失败
        }


    });
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

