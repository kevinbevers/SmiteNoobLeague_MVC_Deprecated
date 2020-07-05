
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

function checkTyping(senderID) {
    timeout = null;
    clearTimeout(timeout);

    timeout = setTimeout(function () {
        //do stuff here
        doneTyping(senderID);
    }, 500);
}



//user is "finished typing," do something
function doneTyping(senderID) {
    //do something
    // create a json object
    // then stringify the whole object
    //dataToPost = JSON.stringify({ methodParam: IntrestList });
    var name = $(senderID).val().replace(/^\s+/, '').replace(/\s+$/, '');;

    if (name != "" && name.length > 3) {
        $.ajax({
            type: "GET",
            url: "/Admin/CheckTeamNameTaken",
            //contentType: "application/json; charset=utf-8", // specify the content type
            dataType: 'JSON',
            data: { teamname: name },
            traditional: true,
            success: function (data) {
                if (data.success == true) {
                    $("#teamnamediv").html('<i style="color:green;" class="fas fa-check-circle errspan" data-toggle="tooltip" data-placement="top" title="Teamname available"></i>');
                }
                else {
                    $("#teamnamediv").html('<i style="color:red;" class="fas fa-times-circle errspan" data-toggle="tooltip" data-placement="top" title="Teamname taken"></i>');
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
    else {
        $("#teamnamediv").html('');
    }
}