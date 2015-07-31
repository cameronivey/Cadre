function loadLayout() {

    $("#navbar").load("/Views/Shared/nav.html");
    $('head').append("/Views/Shared/header.html");
    appendHead();
}

function appendHead() {
    $('head').append('<script src="/scripts/application.js"></s' + 'cript>');
    $('head').append('<script src="/scripts/jquery-2.1.1.min.js"></s' + 'cript>');
    $('head').append('<script src="/scripts/bootstrap.min.js"></s' + 'cript>');
    $('head').append('<script src="/scripts/sb-admin-2.js"></s' + 'cript>');
    $('head').append('<met' + 'a http-equiv="X-UA-Compatible" content="IE=edge">')
    $('head').append('<met' + 'a name="viewport" content="width=device-width, initial-scale=1">')
    $('head').append('<met' + 'a name="description" content="">')
    $('head').append('<met' + 'a name="author" content="">')
    $('head').append('<lin' + 'k href="/CSS/sb-admin-2.css" rel="stylesheet">')
    $('head').append('<lin' + 'k href="/css/font-awesome.min.css" rel="stylesheet" type="text/css">')
    $('head').append('<lin' + 'k rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css">')
    $('head').append('<lin' + 'k rel="stylesheet" type="text/css" href="/CSS/site.css">')
    $('head').append('<script src="/scripts/date.js"></s' + 'cript>');
}

function toggleRegisterForm() {
    $("#registerForm").toggle();
}

function getAllUsers() {
    $.ajax({
        type: 'GET',
        url: '/api/user/getall',
        dataType: 'json',
        success: function (data) {
            var quotedName = "'" + this.Name + "'";
            $.each(data, function () {
                $("#userDisplayTable").append(
                    "<tr><td>" + this.Id +
                    "</td><td>" + this.Name +
                    "</td><td>" + this.Email +
                    "</td><td>" +
                    '<button class ="btn btn-info" onClick="viewUserPosts(' + this.Id + ', \'' + this.Name + '\')">View Posts</button>' +
                    "</td></tr>");
            })
        },
        error: function () {
            alert("fail");
        }
    })
};

function viewUserPosts(id, name) {
    $.ajax({
        type: 'GET',
        url: '/api/post/getuserposts/' + id,
        dataType: 'json',
        success: function (data) {
            var postsHtml = "<h4 class='page-header'>All Posts for " + name + "</h4>";
            if (data.length == 0) {
                    postsHtml += "<span>This user has no posts.</span></br>";
            } else { 
                $.each(data, function () {
                    postsHtml += "<table class='table'>" +
                                 "<tr><td>Post Id: </td><td>" + this.Id + "</td></tr>" +
                                 "<tr><td>Time: </td><td>" + this.TimeSubmitted + "</td></tr>" +
                                 "<tr><td>Summary: </td><td>" + this.Summary + "</td></tr>" +
                                 "<tr><td>Details: </td><td>" + this.Details + "</td></tr>" +
                                 "</table>";
                })
            }
            $("#userPostsView").html(postsHtml);
        },
        error: function () {
            alert("fail");
        }
    })
};

function hideUserPosts(id) {
    var divName = "#user_" + id + "_posts";
    $(divName).remove();
    var hideBtnId = "#hideBtn" + id;
    var viewBtnId = "#viewBtn" + id;
    $(hideBtnId).hide();
    $(viewBtnId).show();
}

function goToIndex() {
    window.location = "/Views/Index.html";
}

function addPost() {
    var postData = {
        summary: $("#summary").val(),
        details: $("#details").val()
    };

    console.log(postData);

    if ($("#typeselect").val() == "Request") {
        $.ajax({
            type: 'POST',
            url: '/api/post/addrequest',
            data: postData
        }).done(function (data) {
            $("#message").show();
            document.getElementById("objectjson").innerHTML = data.summary;
        }).fail(function () {
            alert("ajax fail");
        });
    } else {
        $.ajax({
            type: 'POST',
            url: '/api/post/addannouncement',
            data: postData,
        }).done(function (data) {
            $("#message").show();
            document.getElementById("objectjson").innerHTML = data.summary;
        }).fail(function () {
            alert("ajax fail");
        });
    }
}

function sendDigestEmail() {
    $.ajax({
        type: 'GET',
        url: '/api/email/sendemail/true'
    }).done(function () {
        $("#sentMessage").show();
        $("#sentMessageError").hide();
    }).fail(function () {
        $("#sentMessageError").show();
        $("#sentMessage").hide();
    })
}

function sendReminderEmail() {
    $.ajax({
        type: 'GET',
        url: '/api/email/sendemail/false'
    }).done(function () {
        $("#sentMessage").show();
        $("#sentMessageError").hide();
    }).fail(function () {
        $("#sentMessageError").show();
        $("#sentMessage").hide();
    })
}