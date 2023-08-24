// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    console.log("Page is ready");

    //prevents right click context menu from showing
    $(document).bind("contextmenu", function (event) {
        event.preventDefault();
        Console.log("Prevented context menu from showing");
    });

    //switch case for left or right mouse clicks
    $(document).on("mousedown", ".cell", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault();
                var btn = $(this).val();
                console.log("Left clicked");
                UpdateButton(btn, "/minesweeper/ShowOneButton");
                break;
            case 2:
                console.log("Middle mouse button clicked.");
                break;
            case 3:
                event.preventDefault();
                var btn = $(this).val();
                console.log("Right clicked");
                UpdateButton(btn, "/Minesweeper/RightClickShowOneButton");
                break;
            default:
                alert('nothing');
        }
    });

});

function UpdateButton(rowcol, url) {
    $.ajax({
        type: "json",
        method: 'POST',
        url: url,
        data: {
            "rowcol": rowcol
        },
        success: function (data) {
            console.log(data);
            $(".game-board").html(data);
        }
    });
};
