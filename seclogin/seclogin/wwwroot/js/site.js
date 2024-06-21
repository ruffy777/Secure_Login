// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function UsercredentialPost(event) {
    event.preventDefault();

    console.log("I was here");

    var formData = {
        Email: $('#email').val(),
        Passwort: $('#password').val(),
        Vorname: $('#firstName').val(),
        Nachname: $('#lastName').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Register/Post',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        success: function (response) {
            console.log("Success: ", response);
            // Handle success
        },
        error: function (error) {
            console.error("Error: ", error);
            // Handle error
        }
    });
}

function loginPost(event) {
    event.preventDefault();

    console.log("I was here 2");

    var formData = {
        Email: $('#email').val(),
        Passwort: $('#password').val(),
        Vorname: null,
        Nachname: null
    };

    

    $.ajax({
        type: 'POST',
        url: '/Login/Login',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        success: function (response) {
            console.log("Success: ", response);
            alert("Logged In");
            // Handle success
        },
        error: function (error) {
            console.error("Error: ", error);
            // Handle error
            alert("wrong Password or E-Mail");
        }
    });
}
