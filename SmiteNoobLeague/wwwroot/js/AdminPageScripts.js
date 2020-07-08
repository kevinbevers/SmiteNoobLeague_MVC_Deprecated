/*Team modal scripts*/
function CreateTeamPopUp() {
    var CreateTeamModal = $('#CreateTeamModal');
    var FormContent = $('#FormContentCreateTeam');

    // create a json object
    // then stringify the whole object
    //dataToPost = JSON.stringify({ methodParam: object });

    $.ajax({
        type: "GET",
        url: "/Admin/CreateTeam",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: "html",
        success: function (partialview) {
            FormContent.html("");
            FormContent.html(partialview);
            jQuery.validator.unobtrusive.parse('#CreateTeamForm');
            CreateTeamModal.modal('show');
            CreateTeamModal.hover(function () {
                this.focus();
            });
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function TeamCreatedSuccess() {

    var CreateTeamModal = $('#CreateTeamModal');
    var FormContent = $('#FormContentCreateTeam');
    CreateTeamModal.modal('hide');
    FormContent.html("");
    var succesMessage = '<h3 style="color:green;">Team successfully Created <i class="fas fa-check-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(succesMessage);
    MessageModal.modal('show');

    window.setTimeout(function () {
        MessageModal.modal('hide');
    }, 1600);
}

function TeamCreatedError() {

    var CreateTeamModal = $('#CreateTeamModal');
    var FormContent = $('#FormContentCreateTeam');
    CreateTeamModal.modal('hide');
    FormContent.html("");
    var failedMessage = '<h3 style="color:red;">Something went wrong trying to create a team <i class="fas fa-times-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(failedMessage);
    MessageModal.modal('show');
}

function ManageTeamPopUp() {
    var ManageTeamsModal = $('#ManageTeamModal');
    var Content = ManageTeamsModal.find('.modal-content');

    $.ajax({
        type: "GET",
        url: "/Admin/ManageTeam",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: "html",
        success: function (partialview) {
            Content.html("");
            Content.html(partialview);
            ManageTeamsModal.modal();
        },
        error: function (data) {
            ManageTeamsError('Something went wrong trying to get teams');
        }
    });
}

function DeleteTeam(Id) {
    // create a json object
    // then stringify the whole object
    //dataToPost = JSON.stringify({ methodParam: object });


    //delete the are you sure pop up from the html code
    var modalContainer = $('#dynamicModalContainer');
    var areyousure = modalContainer.find('#PopUpModal');
    areyousure.modal('hide');
    //modalContainer.html("");

    //get the modal view to update
    var ManageTeamsModal = $('#ManageTeamModal');
    var Content = ManageTeamsModal.find('.modal-content');

    $.ajax({
        type: "POST",
        url: "/Admin/DeleteTeam",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: 'html',
        data: { id: Id },
        traditional: true,
        success: function (partialview) {
            Content.html(partialview);
        },
        error: function (data) {
            ManageTeamsError('Something went wrong trying to delete the team');
        }
    });
}

function ManageTeamsError(msg) {

    var ManageTeamModal = $('#ManageTeamModal');
    ManageTeamModal.modal('hide');

    var failedMessage = '<h3 style="color:red;">' + msg + '<i class="fas fa-times-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(failedMessage);
    MessageModal.modal('show');
}

function EditTeamPopUp(Id) {
    var ManageTeamsModal = $('#ManageTeamModal');
    var Content = ManageTeamsModal.find('.modal-content');

    $.ajax({
        type: "GET",
        url: "/Admin/EditGetTeam",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: "html",
        data: { id: Id },
        success: function (partialview) {
            Content.html("");
            Content.html(partialview);
            jQuery.validator.unobtrusive.parse('#EditTeamForm');
        },
        error: function (data) {
            ManageTeamsError('Something went wrong trying to get team info');
        }
    });
}

function TeamEditSuccess() {
    var ManageTeamModal = $('#ManageTeamModal');
    ManageTeamModal.modal('hide');

    var succesMessage = '<h3 style="color:green;">Team successfully Edited <i class="fas fa-check-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(succesMessage);
    MessageModal.modal('show');

    window.setTimeout(function () {
        MessageModal.modal('hide');
        ManageTeamPopUp();
    }, 1600);
}
/*Are you sure pop up*/
function AreYouSureTeam(id,teamname) {

   
    var PopUpModal = '<div class="modal fade" id="PopUpModal" tabindex="-1" role="dialog" aria-hidden="true">' +
        '<div class="modal-dialog" role="document">' +
            '<div class="modal-content">' +
                '<div class="modal-body text-center">' +
                     '<h5>Are you sure you want to delete ' + teamname + '?</h4>' +
                     '<p>there is no going back!</p>' +
                '</div>' +
                '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>' +
                    '<button type="button" class="btn btn-danger" onclick="DeleteTeam(' + id + ')">Delete Team</button>' +
                '</div>' +
            '</div>' +
    '</div>' +
        '</div';

    var modalContainer = $('#dynamicModalContainer');        
    modalContainer.html("");
    modalContainer.append(PopUpModal);
    var areyousure = modalContainer.find('#PopUpModal');
    areyousure.modal('show');
}
function DetailedTeamCreate(toggle) {
    var CreateTeamModal = $('#CreateTeamModal');
    var FormContent = $('#FormContentCreateTeam');

    if (toggle == true) {
        $.ajax({
            type: "POST",
            url: "/Admin/CreateTeamWithMembers",
            data: $('#CreateTeamForm').serialize(),   //your form name.it takes all the values of model   
            //contentType: "application/json; charset=utf-8", // specify the content type
            dataType: "html",
            success: function (partialview) {
                FormContent.html("");
                FormContent.html(partialview);
                jQuery.validator.unobtrusive.parse('#CreateTeamForm');
                CreateTeamModal.modal('show');
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Admin/CreateTeamWithoutMembers",
            data: $('#CreateTeamForm').serialize(),   //your form name.it takes all the values of model   
            //contentType: "application/json; charset=utf-8", // specify the content type
            dataType: "html",
            success: function (partialview) {
                FormContent.html("");
                FormContent.html(partialview);
                jQuery.validator.unobtrusive.parse('#CreateTeamForm');
                CreateTeamModal.modal('show');
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
}
function DetailedTeamEdit(toggle) {
    var ManageTeamsModal = $('#ManageTeamModal');
    var Content = ManageTeamsModal.find('.modal-content');

    if (toggle == true) {
        $.ajax({
            type: "POST",
            url: "/Admin/EditGetDetailedTeam",
            data: $('#EditTeamForm').serialize(),   //your form name.it takes all the values of model   
            //contentType: "application/json; charset=utf-8", // specify the content type
            dataType: "html",
            success: function (partialview) {
                Content.html("");
                Content.html(partialview);
                jQuery.validator.unobtrusive.parse('#EditTeamForm');
            },
            error: function (data) {
                console.log(data);
                ManageTeamsError('Something went wrong trying to get team info');
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Admin/EditGetBasicTeam",
            data: $('#EditTeamForm').serialize(),   //your form name.it takes all the values of model   
            //contentType: "application/json; charset=utf-8", // specify the content type
            dataType: "html",
            success: function (partialview) {
                Content.html("");
                Content.html(partialview);
                jQuery.validator.unobtrusive.parse('#EditTeamForm');
            },
            error: function (data) {
                console.log(data);
                ManageTeamsError('Something went wrong trying to get team info');
            }
        });
    }
}
/*Remove content from create team modal when getting closed*/
$('#CreateTeamModal').on('hidden.bs.modal', function () {
    // do something…
    var FormContent = $('#FormContentCreateTeam');
    FormContent.html("");
});
/*Account modal scripts*/
function CreateAccountPopUp() {
    var AccountModal = $('#AccountModal');
    var Content = AccountModal.find('.modal-content');

    $.ajax({
        type: "GET",
        url: "/Admin/CreateAccount",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: "html",
        //data: { id: Id },
        success: function (partialview) {
            Content.html("");
            Content.html(partialview);
            jQuery.validator.unobtrusive.parse('#AccountForm');
            AccountModal.modal('show');
        },
        error: function (data) {
            //something went wrong
            console.log(data);
            ManageAccountError("Something went wrong during account creation");

        }
    });
}
function ManageAccountError(msg) {
    var AccountModal = $('#AccountModal');
    AccountModal.modal('hide');
    var Content = AccountModal.find('.modal-content');
    Content.html("");
    var failedMessage = '<h3 style="color:red;">' + msg + '<i class="fas fa-times-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(failedMessage);
    MessageModal.modal('show');
}
function AccountCreateSuccess() {
    var AccountModal = $('#AccountModal');
    AccountModal.modal('hide');
    var Content = AccountModal.find('.modal-content');
    Content.html("");

    var succesMessage = '<h3 style="color:green;">Account successfully created <i class="fas fa-check-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(succesMessage);
    MessageModal.modal('show');

    window.setTimeout(function () {
        MessageModal.modal('hide');
    }, 1600);
}
function EditAccountPopUp(Id) {
    var AccountModal = $('#AccountModal');
    var Content = AccountModal.find('.modal-content');

    $.ajax({
        type: "GET",
        url: "/Admin/EditGetAccount",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: "html",
        data: { id: Id },
        success: function (partialview) {
            Content.html("");
            Content.html(partialview);
            jQuery.validator.unobtrusive.parse('#AccountForm');
        },
        error: function (data) {
            ManageAccountError('Something went wrong trying to get account info');
        }
    });
}
function ManageAccountPopUp() {
    var AccountModal = $('#AccountModal');
    var Content = AccountModal.find('.modal-content');
    Content.html("");

    $.ajax({
        type: "GET",
        url: "/Admin/ManageAccount",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: "html",
        success: function (partialview) {
            Content.html("");
            Content.html(partialview);
            AccountModal.modal();
        },
        error: function (data) {
            ManageTeamsError('Something went wrong trying to get accounts');
        }
    });
}
function AreYouSureAccount(id, accountname) {


    var PopUpModal = '<div class="modal fade" id="PopUpModal" tabindex="-1" role="dialog" aria-hidden="true">' +
        '<div class="modal-dialog" role="document">' +
        '<div class="modal-content">' +
        '<div class="modal-body text-center">' +
        '<h5>Are you sure you want to delete ' + accountname + '?</h4>' +
        '<p>there is no going back!</p>' +
        '</div>' +
        '<div class="modal-footer">' +
        '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>' +
        '<button type="button" class="btn btn-danger" onclick="DeleteAccount(' + id + ')">Delete Account</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div';

    var modalContainer = $('#dynamicModalContainer');
    modalContainer.html("");
    modalContainer.append(PopUpModal);
    var areyousure = modalContainer.find('#PopUpModal');
    areyousure.modal('show');
}
function DeleteAccount(Id) {
    // create a json object
    // then stringify the whole object
    //dataToPost = JSON.stringify({ methodParam: object });


    //delete the are you sure pop up from the html code
    var modalContainer = $('#dynamicModalContainer');
    var areyousure = modalContainer.find('#PopUpModal');
    areyousure.modal('hide');
    //modalContainer.html("");

    //get the modal view to update
    var AccountModal = $('#AccountModal');
    var Content = AccountModal.find('.modal-content');

    $.ajax({
        type: "POST",
        url: "/Admin/DeleteAccount",
        //contentType: "application/json; charset=utf-8", // specify the content type
        dataType: 'html',
        data: { id: Id },
        traditional: true,
        success: function (partialview) {
            Content.html(partialview);
        },
        error: function (data) {
            ManageTeamsError('Something went wrong trying to delete the account');
        }
    });
}
function AccountEditSuccess() {
    var AccountModal = $('#AccountModal');
    AccountModal.modal('hide');

    var succesMessage = '<h3 style="color:green;">Account successfully Edited <i class="fas fa-check-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(succesMessage);
    MessageModal.modal('show');

    window.setTimeout(function () {
        MessageModal.modal('hide');
        ManageAccountPopUp();
    }, 1600);
}
function EditAccountFailed() {
    ManageAccountError("Something went wrong trying to save changes");
}
/*PlayerScripts*/
function SearchPlayer(targetInputID) {
    var inputID = targetInputID + "Name";
    var playername = $(inputID).val();
    var modalObj = $('.bd-TeamMemberSearchModal');
    if (playername != "") {
        // do a jquery call to get playerid, platform and name by playername via the Smite API
        modalObj.find('.modal-title').text('Players found with name: ' + playername);
        modalObj.find('.modal-body').html("");
        modalObj.find('.modal-body').append("<ul>");
        modalObj.find('.modal-body').append('<li><a onclick="SendBackID(' + 2 + ",'" + targetInputID + "','" + playername + "','" + 9 + "'" + ')" href="#">' + playername + ' Playstation</a></li>');
        modalObj.find('.modal-body').append('<li><a onclick="SendBackID(' + 3 + ",'" + targetInputID + "','" + playername + "','" + 1 + "'" + ')" href="#">' + playername + ' PC</a></li>');
        modalObj.find('.modal-body').append("</ul>");
        modalObj.modal();
    }
    else {
        //do nothing / tell user that a name needs to be filled in
    }
}
function SendBackID(id, targetInputID, name, platformID) {

    var ID = targetInputID + "ID";
    var Name = targetInputID + "Name";
    var Platform = targetInputID + "PlatformID";
    var Btn = targetInputID + "Button";

    $(ID).val(id);
    $(Name).val(name);
    $(Name).prop('readOnly', true);
    $(Name).addClass('disabled');
    $(Platform).val(platformID);

    $(Btn).removeClass('btn-primary');
    $(Btn).addClass('btn-danger');
    $(Btn).html("Remove player");
    $(Btn).attr("onclick", "RemovePlayer('" + targetInputID + "')");

    var modalObj = $('.bd-TeamMemberSearchModal');
    modalObj.modal('hide');
}
function RemovePlayer(targetInputID) {

    var ID = targetInputID + "ID";
    var Name = targetInputID + "Name";
    var Platform = targetInputID + "PlatformID";
    var Btn = targetInputID + "Button";

    $(ID).val('');
    $(Name).val('');
    $(Name).prop('readOnly', false);
    $(Name).removeClass('disabled');
    $(Platform).val('');

    $(Btn).removeClass('btn-danger');
    $(Btn).addClass('btn-primary');
    $(Btn).html("Search player");
    $(Btn).attr("onclick", "SearchPlayer('" + targetInputID + "')");
}