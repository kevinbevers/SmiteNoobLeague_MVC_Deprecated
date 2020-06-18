// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//move all these functions to an adminpage javascript file so it is not accessible on the whole site
function SearchPlayer(inputID,targetInputID) {
    var playername = $(inputID).val();
    var modalObj = $('.bd-TeamMemberSearchModal');
    if (playername != "") {       
        // do a jquery call to get playerid, platform and name by playername via the Smite API
        modalObj.find('.modal-title').text('Players found with name: ' + playername);
        modalObj.find('.modal-body').html("");
        modalObj.find('.modal-body').append("<ul>");
        modalObj.find('.modal-body').append('<li><a onclick="SendBackID(' + 2 + ",'" + targetInputID + "','" + playername + "'" + ')" href="#">' + playername + ' Playstation</a></li>');
        modalObj.find('.modal-body').append('<li><a onclick="SendBackID(' + 3 + ",'" + targetInputID + "','" + playername + "'" + ')" href="#">' + playername + ' PC</a></li>');
        modalObj.find('.modal-body').append("</ul>");
        modalObj.modal();
    }
    else
    {
        //do nothing / tell user that a name needs to be filled in
    }
}

function SendBackID(id, targetInputID,name) {

    var ID = targetInputID + "ID";
    var Name = targetInputID + "Name";

    $(ID).val(id);
    $(Name).val(name);
    var modalObj = $('.bd-TeamMemberSearchModal');
    modalObj.modal('hide');
}

function CreateTeamPopUp() {
    var CreateTeamModal = $('#CreateTeamModal');
    var FormContent = $('#FormContentCreateTeam')

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
                CreateTeamModal.modal();
            },
            error: function (data) {
                console.log(data);
            }
        });
}

function TeamCreatedSuccess() {

    var CreateTeamModal = $('#CreateTeamModal');
    CreateTeamModal.modal('hide');
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
    CreateTeamModal.modal('hide');

    var failedMessage = '<h3 style="color:red;">Something went wrong trying to create a team <i class="fas fa-times-circle"></i></h3>';
    var MessageModal = $('#MessageModal');
    MessageModal.find('.modal-body').html("");
    MessageModal.find('.modal-body').append(failedMessage);
    MessageModal.modal('show');    
}

function CreateManagePopUp() {
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

        var ManageTeamsModal = $('#ManageTeamModal');
        var Content = ManageTeamsModal.find('.modal-body');

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
                ManageTeamsError('Something went wrong trying to deleting the team');               
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

function CreateEditTeamPopUp(Id) {
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