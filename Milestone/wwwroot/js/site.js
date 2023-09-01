// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
$(function () {
    console.log("Page is ready");
    //prevents right click context menu from showing
    $(document).bind("contextmenu", function (event) {
        event.preventDefault();
        console.log("Prevented context menu from showing");
    });

    //initiates partial view
    //$(document).on("click", ".save-game-button", function () {
    //    console.log("You click the save game button");
    //    var SaveGame = {
    //        /*       "UserId": $("#modal-input-userId").val(),
    //               "GameId": $("#modal-input-gameId").val(),
    //               "GameName": $("#modal-input-gameName").val(),
    //               "LiveSites": $("#modal-input-liveSites").val(),
    //               "Time": $("#modal-input-time").val(),
    //               "Date": $("#modal-input-date").val(),
    //               "ButtonStates": $("#modal-input-buttonStates").val(),
            
    //        */     //for testing
    //        "UserId": 999,
    //        "GameId": 999,
    //        "GameName": "test",
    //        "LiveSites": "12+34+56+78",
    //        "Time": "test",
    //        "Date": "test",
    //        "ButtonStates": "87+65+43+21",
    //    }

    //    $.ajax({
    //        type: 'json',
    //        data: SaveGame,
    //        url: '/Minesweeper/RetrieveSavedGameModelProperties',
    //        success: function (data) {
    //            console.log("Calling: RetrieveSavedGameModelProperties");
    //        }
    //    })
    //});

    //reads from partial view fields and passes them to controller
    $("#save-button").click(function () {
        // get the values of the input fields and make a product JSON object
        var SaveGame = {
            "GameName": $("#modal-input-gamename").val()
        }

        //Save the update product record in the database via controller
        $.ajax({
            type: 'json',
            data: SaveGame,
            url: '/Minesweeper/SaveGame',
            success: function (data) {
                //show partial update for testing purposes
                console.log("Saved to repository.");
                console.log(data)

            }
        })
        //location.reload();
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

var counter = 0;
var interval = null;
function StartTimer() {
    if (interval == null) {
        interval = setInterval(function () {
            counter += 1;
            document.getElementById("count").innerHTML = counter;
        }, 1000);
    }
}

function StopTimer() {
    clearInterval(interval);
    interval = null;
    document.getElementById("count").innerHTML = counter;
}

window.onload = function (e) {
    document.getElementById("gamemusic").play();
}

window.onunload = function (e) {
    StopTimer();
}