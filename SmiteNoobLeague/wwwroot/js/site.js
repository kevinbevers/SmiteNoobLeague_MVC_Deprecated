// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SearchPlayer(inputID,targetInputID) {
    var playername = $(inputID).val();
    var modalObj = $('.bd-TeamMemberSearchModal');
    if (playername != "") {       
        // do a jquery call to get playerid, platform and name by playername via the Smite API
        modalObj.find('.modal-title').text('Players found with name: ' + playername);
        modalObj.find('.modal-body').html("");
        modalObj.find('.modal-body').append('<a onclick="SendBackID(' + 2 + ",'" + targetInputID + "'" + ')" href="#">Playername playerid</a>');
        modalObj.find('.modal-body').append('<a onclick="SendBackID(' + 3 + ",'" + targetInputID + "'" + ')" href="#">Playername playerid</a>');
        modalObj.modal();
    }
    else
    {
        //do nothing / tell user that a name needs to be filled in
    }
}

function SendBackID(id, targetInputID) {

    $(targetInputID).val(id);

    var modalObj = $('.bd-TeamMemberSearchModal');
    modalObj.modal('hide');
}