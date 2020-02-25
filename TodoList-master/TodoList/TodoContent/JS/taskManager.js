var taskManager = (function () {
    
    function UpdatableRow(event) {
        var closestTr = $(event.target).closest("tr");
        closestTr.find(".read-only").addClass("u-hide");
        closestTr.find(".update-only").removeClass("u-hide");
        closestTr.find("#lnkEditTask").addClass("u-hide");
        closestTr.find("#lnkRemoveTask").addClass("u-hide");
        closestTr.find("#lnkUpdateTask").removeClass("u-hide");
        closestTr.find("#lnkCancelUpdate").removeClass("u-hide");
        closestTr.find("#ddlStatuses").val(closestTr.find("#lblStatus").data("status")).change();
    }

    function ReadonlyRow(event) {
        var closestTr = $(event.target).closest("tr");
        closestTr.find(".read-only").removeClass("u-hide");
        closestTr.find(".update-only").addClass("u-hide");
        closestTr.find("#lnkEditTask").removeClass("u-hide");
        closestTr.find("#lnkRemoveTask").removeClass("u-hide");
        closestTr.find("#lnkUpdateTask").addClass("u-hide");
        closestTr.find("#lnkCancelUpdate").addClass("u-hide");
    }

    function UpdateSuccessCallback(responce, closestTr, taskId) {
        ReadonlyRow(closestTr);

        $('table tr[data-TaskId="' + taskId + '"] .task-name').html(responce.Name);
        $('table tr[data-TaskId="' + taskId + '"] .task-description').html(responce.Description);

        var status = "";
        if (responce.Status == 1)
            status = 'ToDo';
        else if (responce.Status == 2)
            status = 'In-Progress';
        else if (responce.Status == 3)
            status = 'Completed';

        $('table tr[data-TaskId="' + taskId + '"] #lblStatus').html(status);
        $('table tr[data-TaskId="' + taskId + '"] #lblStatus').data("status", responce.Status);
        $('table tr[data-TaskId="' + taskId + '"] #lblUpdatedDate').html(GetDate(responce.UpdatedDate));

        $('#successMessage').css('display', 'block');
        $('#successMessage').html('Task updated successfully.');
    }

    function UpdateTask(row) {
        var taskId = row.attr('data-taskid');
        var closestTr = row.closest("tr");
        var projectType = closestTr.find('#lblProjectName').attr('data-ProjectType');
        var taskName = closestTr.find("#txtTaskName");
        var taskDescription = closestTr.find("#txtTaskDescription");
        var taskStatus = closestTr.find("#ddlStatuses option:selected");
        var createdDate = closestTr.find("#lblCreatedDate").attr("data-createddate");

        if (projectType == '1' && ValidateAgileTasks() == true) {

        }

        if (projectType == '2' && ValidateNormalTasks(taskName, taskDescription, closestTr.find("#ddlStatuses")) == true) {

            var todoTask = {
                todoTaskModel:
                {
                    Id: taskId,
                    ProjectId: closestTr.find('#lblProjectName').attr('data-ProjectId'),
                    Name: taskName.val(),
                    Description: taskDescription.val(),
                    Status: taskStatus.val(),
                    CreatedDateString: createdDate
                }
            };

            $.ajax({
                url: updateNormalTask,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(todoTask),
                cache: false,
                success: function (responce) {
                    UpdateSuccessCallback(responce, closestTr, taskId);
                },
                error: function (ex) {
                    $('#errorMessage').css('display', 'block');
                    $('#errorMessage').html('Error while processing data.');
                }
            });
        }
    }

    function GetDate(dateString) {
        var dateString = dateString.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = ("0" + (currentTime.getMonth() + 1)).slice(-2);
        var day = ("0" + currentTime.getDate()).slice(-2);
        var year = currentTime.getFullYear();
        var date = day + "/" + month + "/" + year;
        return date;
    }

    function PopulateTaskListRow(responce, isInsert) {
        var newRow = '<td>';
        newRow += '<input type="checkbox" data-TaskId="' + responce.Id + '" />';
        newRow += '</td>';

        newRow += '<td>';
        newRow += '<label>' + responce.Name + '</label>';
        newRow += '</td>';

        newRow += '<td>';
        newRow += '<label>' + responce.Description + '</label>';
        newRow += '</td>';

        newRow += '<td class="u-hide">';
        newRow += '<label>' + projectName + '</label>';
        newRow += '</td>';

        newRow += '<td>';
        if (responce.Status == 1)
            newRow += '<label>ToDo</label>';
        else if (responce.Status == 2)
            newRow += '<label>In-Progress</label>';
        else if (responce.Status == 3)
            newRow += '<label>Completed</label>';
        newRow += '</td>';

        var createdDate = responce != null ? new Date(parseInt(responce.CreatedDate.substr(6, 13))) : new Date();
        var formattedCreatedDate = createdDate.getDate() + "/" + (createdDate.getMonth() + 1) + "/" +
            createdDate.getFullYear();

        newRow += '<td>';
        newRow += '<label>' + formattedCreatedDate + '</label>';
        newRow += '</td>';

        var modifiededDate = "-";
        var formattedDate = "";

        if (isInsert == false) {
            modifiededDate = responce != null ? new Date(parseInt(responce.UpdatedDate.substr(6, 13))) : new Date();

            formattedDate = modifiededDate.getDate() + "/" + (modifiededDate.getMonth() + 1) + "/" +
                modifiededDate.getFullYear();
        }

        newRow += '<td>';
        newRow += '<label>' + formattedDate + '</label>';
        newRow += '</td>';

        newRow += '<td>';
        newRow += '<a id="lnkUpdateTask" class="glyphicon glyphicon-floppy-disk u-hide" href="#" data-ProjectId="' + prjectId + '"' +
            'data-toggle="tooltip" data-placement="bottom" title="Update"' +
            'data-CreatedDate="' + responce.CreatedDate + '" data-CreatedBy="' + responce.CreatedBy +'"' +
            'data-ProjectName="' + projectName + '" data-ProjectType="' + projectType + '" data-TaskId="' + responce.Id + '"></a>';
        newRow += '<span class="m-todo-icon-space"></span>';
        newRow += '<a id="lnkCancelUpdate" class="glyphicon glyphicon-ban-circle u-hide" href="#" data-ProjectType="' + projectType + '"' +
            'data-toggle="tooltip" data-placement="bottom" title="Cancel"' +
            'data-ProjectName="' + projectName + '" data-ProjectType="' + projectType + '" data-TaskId="' + responce.Id + '"></a>';
        newRow += '<a id="lnkEditTask" class="glyphicon glyphicon-pencil" href="#" data-ProjectId="' + prjectId + '"' +
            'data-toggle="tooltip" data-placement="bottom" title="Update"' +
            'data-ProjectName="' + projectName + '" data-ProjectType="' + projectType + '" data-TaskId="' + responce.Id + '"></a>';
        newRow += '<span class="m-todo-icon-space"></span>';
        newRow += '<a id="lnkRemoveTask" class="glyphicon glyphicon-trash" href="#" data-ProjectType="' + projectType + '"' +
            'data-toggle="tooltip" data-placement="bottom" title="Update"' +
            'data-ProjectName="' + projectName + '" data-ProjectType="' + projectType + '" data-TaskId="' + responce.Id + '"></a>';
        newRow += '</td>';

        return newRow;
    }

    function ValidateAgileTasks() {
        var flag = true;
        if ($('#txtName').val().trim() == '') {
            $('#txtName').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#txtName').css("border-color", "initial");
        }

        if ($('#txtDescription').val().trim() == '') {
            $('#txtDescription').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#txtDescription').css("border-color", "initial");
        }

        if ($('#ddlStatuses').val() == '0') {
            $('#ddlStatuses').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#ddlStatuses').css("border-color", "initial");
        }

        if ($('#txtProjectName').val() == '') {
            $('#txtProjectName').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#txtProjectName').css("border-color", "initial");
        }

        if ($('#txtEfforts').val() == '') {
            $('#txtEfforts').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#txtEfforts').css("border-color", "initial");
        }

        if ($('#txtStoryPoints').val() == '') {
            $('#txtStoryPoints').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#txtStoryPoints').css("border-color", "initial");
        }

        if ($('#txtBurnedHours').val() == '') {
            $('#txtBurnedHours').css("border-color", "Red");
            flag = false;
        }
        else {
            $('#txtBurnedHours').css("border-color", "initial");
        }

        return flag;
    }

    function ValidateNormalTasks(taskName, taskDescription, taskStatus) {
        var flag = true;
        if (taskName.val().trim() == '') {
            taskName.css("border-color", "Red");
            flag = false;
        }
        else {
            taskName.css("border-color", "initial");
        }

        if (taskDescription.val().trim() == '') {
            taskDescription.css("border-color", "Red");
            flag = false;
        }
        else {
            taskDescription.css("border-color", "initial");
        }

        if (taskStatus.find("option:selected").val() == '0') {
            taskStatus.css("border-color", "Red");
            flag = false;
        }
        else {
            taskStatus.css("border-color", "initial");
        }

        return flag;
    }

    function SubmitNormalTask() {
        var projectType = $('#lnkCreateNew').data('projecttype');
        var projectId = $("#lnkCreateNew").data("projectid");
        var taskName = $("#txtName");
        var taskDescription = $("#txtDescription");
        var taskStatus = $("#ddlTaskStatus");

        if (projectType == '2' && ValidateNormalTasks(taskName, taskDescription, taskStatus) == true) {

            var todoTask = {
                todoTaskModel:
                {
                    ProjectId: projectId,
                    Name: taskName.val(),
                    Description: taskDescription.val(),
                    Status: taskStatus.find("option:selected").val()
                }
            };

            $.ajax({
                url: insertNormalTask,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(todoTask),
                cache: false,
                success: function (responce) {
                    var newRow = '<tr data-TaskId="' + responce.Id + '">';
                    newRow += PopulateTaskListRow(responce, true);
                    newRow += '</tr>';

                    $('#table-all-tasks').append(newRow);

                    $("#SaveTaskPopup").dialog('close');

                    $('#successMessage').css('display', 'block');
                    $('#successMessage').html('Task added successfully.');
                },
                error: function () {
                    $('#errorMessage').css('display', 'block');
                    $('#errorMessage').html('Error while processing data.');
                }
            });
        }
    }

    function deleteTask(event) {
        var taskId = $(event).attr('data-taskid');
        var del = confirm("Do you want to delete this record?");
        if (del == true) {
            $.ajax({
                url: deleteNormalTask,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    taskId: taskId
                }),
                success: function () {
                    $('table tr[data-TaskId="' + taskId + '"]').remove();

                    $('#successMessage').css('display', 'block');
                    $('#successMessage').html('Task deleted successfully.');
                },
                error: function (ex) {
                    $('#errorMessage').css('display', 'block');
                    $('#errorMessage').html('Error while processing data.');
                }
            });
        }

        return del;
    }

    function closeDialog() {
        $("#SaveTaskPopup").dialog('close');
    }

    function openDialog() {
        $("#SaveTaskPopup").dialog('open');
        $("#SaveTaskPopup").removeClass("u-hide");
        $("#txtEstimatedCompletionDate").datepicker();
    }

    function deleteTasks(event) {
        var taskIds = new Array();
        if ($('table tr td input[type="checkbox"]:checked').length > 0) {
            $('table tr td input[type="checkbox"]:checked').each(function () {
                taskIds.push($(event).attr('data-taskid'))
            });
            $.ajax({
                url: deleteTasks,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    taskIds: taskIds
                }),
                success: function () {
                    $('table tr td input[type="checkbox"]:checked').each(function () {
                        $('table tr[data-TaskId="' + $(event).attr('data-taskid') + '"]').remove();
                    });

                    $('#successMessage').css('display', 'block');
                    $('#successMessage').html('Task(s) deleted successfully.');
                },
                error: function () {
                    $('#errorMessage').css('display', 'block');
                    $('#errorMessage').html('Error while processing data.');
                }
            });
        }
    }

    function initialize() {
        taskStatuses = JSON.parse(taskStatuses);
        taskPriorities = JSON.parse(taskPriorities);
        $('[data-toggle="tooltip"]').tooltip();

        $("#SaveTaskPopup").dialog({
            height: 333,
            modal: true,
            resizable: true,
            dialogClass: 'no-close success-dialog',
            autoOpen: false,
            position: { my: 'left-210', at: 'top+150' },
            width: "auto",
            open: function (event, ui) {
                originalContent = $("#SaveTaskPopup").html();
            },
            close: function (event, ui) {
                $("#SaveTaskPopup").html(originalContent);
            }
        });

        $(document).on("click", "#btnSubmit", SubmitNormalTask);

        $(document).on("click", "#btnCancel", closeDialog);

        $(document).on("click", "#lnkCreateNew", openDialog);

        $(document).on("click", "table tr td #lnkEditTask", UpdatableRow);

        $(document).on("click", "#btnDeleteTasks", deleteTasks);

        $(document).on("click", "table tr td #lnkCancelUpdate", ReadonlyRow);

        $(document).on("click", "table tr td #lnkUpdateTask", UpdateTask);

        $(document).on("click", "table tr td #lnkRemoveTask", deleteTask);
    }

    return {
        initialize: initialize
    }
})();

$(taskManager.initialize);