/*------------------------------------------------------------------
[JavaScript File]

Project:	Wochenplaner
Version:	1.2
Last change:	11/11/'14 [function print added]
Assigned to:	Tobias Lahmann
Primary use:	MainPage
-------------------------------------------------------------------*/

function openInputOverlay() {
    var $overlay = $('#overlay-appointment-wrapper');
    $overlay.fadeIn();
    $overlay.find('#tbTitle').focus();
}

function openTimeOverlay() {
    var $overlay = $('#overlay-dateTime-wrapper');
    $overlay.fadeIn();
}

function openLoginOverlay() {
    var $overlay = $('#overlay-login-wrapper');
    $overlay.fadeIn();
    $overlay.find('#overlayTextBoxLogin').focus();
}

function closeInputOverlay() {
    var $overlay = $('#overlay-appointment-wrapper');
    $overlay.fadeOut();
}

//$("Calendar").click(function (event) {
//    event.preventDefault();
//});

window.printPage = function () {
    window.print();
}