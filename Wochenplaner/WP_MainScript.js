/*------------------------------------------------------------------
[JavaScript File]

Project:	Wochenplaner
Version:	1.1
Last change:	29/10/'14 [fade in login overlay added]
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