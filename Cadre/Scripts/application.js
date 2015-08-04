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
    $('head').append('<lin' + 'k rel="icon" type="image/png" href="/Content/logo_fav.png" />')
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
                    '<button class="btn btn-info" onClick="viewUserPosts(' + this.Id + ', \'' + this.Name + '\')">View Posts</button></td><td>' +
                    '<button class="btn btn-danger" onClick="removeUser(' + this.Id + ')">Remove</button></td>' +
                    "</td></tr>");
            })
        },
        error: function () {
            alert("fail");
        }
    })
};

function getAllPosts() {
    $.ajax({
        type: 'GET',
        url: '/api/post/getall',
        dataType: 'json',
        success: function (data) {
            var quotedName = "'" + this.Name + "'";
            $.each(data, function () {
                $("#postDisplayTable").append(
                    "<tr><td><button class='btn btn-link' style='text-align:left'  onclick='viewPostInfo(" + this.Id + ")'>" + this.Id + "</button>" +
                    "</td><td>" + this.SubmitterName +
                    "</td><td>" + this.TimeSubmitted +
                    "</td><td>" + this.Summary +
                    "</td><td>" + this.Details +
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
            $("#userPostsHeader").html("All Posts for " + name);
            if (data.length == 0) {
                $("#userPostsTable").html("<span>This user has no posts.</span></br>");
            } else {
                $("#userPostsTable").html("<tr><th>Id</th><th>Time</th><th>Summary</th><th>Details</th></tr>");
                $.each(data, function () {
                    $("#userPostsTable").append("<tr><td>" + this.Id + "</td>" +
                                              "<td>" + this.TimeSubmitted + "</td>" +
                                              "<td>" + this.Summary + "</td>" +
                                              "<td>" + this.Details + "</td></tr>");
                })
            }
        },
        error: function () {
            alert("fail");
        }
    })
};

function removeUser(id) {
    $.ajax({
        type: 'POST',
        url: '/api/user/remove/' + id,
        dataType: 'json',
        success: function () {
            window.location = "/Views/AllUsers.html";
        },
        error: function () {
            alert("remove fail");
        }
    })
}

function goToIndex() {
    window.location = "/Views/Index.html";
}

function addPost() {
    var postData = {
        summary: $("#summary").val(),
        details: $("#details").val()
    };

    if ($("#typeselect").val() == "Request") {
        $.ajax({
            type: 'POST',
            url: '/api/post/addrequest',
            data: postData
        }).done(function () {
            $("#postAdded").show();
            $("#postAddedError").hide();
        }).fail(function () {
            $("#postAddedError").show();
            $("#postAdded").hide();
        })
    } else {
        $.ajax({
            type: 'POST',
            url: '/api/post/addannouncement',
            data: postData,
        }).done(function () {
            $("#postAdded").show();
            $("#postAddedError").hide();
        }).fail(function () {
            $("#postAddedError").show();
            $("#postAdded").hide();
        })
    }
}

function sendDigestEmail() {
    $("#sentMessage").hide();
    $("#sentMessageError").hide();
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
    $("#sentMessage").hide();
    $("#sentMessageError").hide();
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

function getDigestHtml() {
    $.ajax({
        url: "/views/emailformat.html",
        success: function (result) {
            alert(result);
        }
    });
};

function loadPosts(val) {
    if (val == "Week") {
        getWeekPosts();
    } else if (val == "Month") {
        getMonthPosts();
    } else {
        getAllPosts();
    }
}

function getWeekPosts() {
    $.ajax({
        type: 'GET',
        url: '/api/post/getallinlastweek',
        dataType: 'json',
        success: function (data) {
            appendPostsToTable(data);
        },
        error: function () {
            alert("fail");
        }
    })
}

function getMonthPosts() {
    $.ajax({
        type: 'GET',
        url: '/api/post/getallinlastmonth',
        dataType: 'json',
        success: function (data) {
            appendPostsToTable(data);
        },
        error: function () {
            alert("fail");
        }
    })
}

function getAllPosts() {
    $.ajax({
        type: 'GET',
        url: '/api/post/getall',
        dataType: 'json',
        success: function (data) {
            appendPostsToTable(data);
        },
        error: function () {
            alert("fail");
        }
    })
};

function appendPostsToTable(data) {
    var htmlToAppend = "<tr><th>Id</th><th>Name</th><th>Date</th><th>Summary</th><th>Details</th></tr>";
    $.each(data, function () {
        htmlToAppend +=
            "<tr><td><button class='btn btn-link' style='text-align:left'  onclick='viewPostInfo(" + this.Id + ")'>" + this.Id + "</button>" +
            "</td><td>" + this.SubmitterName +
            "</td><td>" + this.TimeSubmitted +
            "</td><td>" + this.Summary +
            "</td><td>" + this.Details +
            "</td></tr>";
    })
    $("#postDisplayTable").html(htmlToAppend);
}