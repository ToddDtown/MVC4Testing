
$(document).ready(function () {

    $("#results-per-page").change(function () {
        window.location.href = $("#results-per-page option:selected").val();
    });

    $("#mi-admin-grid tbody tr").click(function () {

        $.ajax({
            url: "/mi/geteditmodal?id=" + $(this).data("id"),
            type: "GET"
        }).done(function (data) {
            var editModal = $("#initiative-edit-modal");
            if (editModal.length > 0) {
                editModal.html(data);
                editModal.center();
                editModal.fadeTo(300, 1);
            }
        }).fail(function () {
            alert("Edit modal dialog failed to load.");
        });
        
    });

});

var CancelClick = function() {
    var editModal = $("#initiative-edit-modal");
    if (editModal.length > 0) {
        editModal.fadeOut(300);
    }
}

jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", "150px");
    this.css("left", "150px");
    return this;
}

// Highlight the current menu item
var HighlightMenuItem = function (menuItemId) {
    if (menuItemId && menuItemId != "") {
        var menuItem = $("#" + menuItemId);
        if (menuItem.length > 0) {
            menuItem.css("background-color", "#f7581e");
        }
    }
};

// Check if the date entered is valid
function IsValidDate(date) {
    if (typeof date == "undefined" || date == null || date == "") {
        return false;
    }
    return date.match(dateReg = /^[0,1]?\d\/(([0-2]?\d)|([3][01]))\/((199\d)|([2-9]\d{3}))\s[0-2]?[0-9]:[0-5][0-9]:[0-5][0-9] (AM|PM)?$/);
};

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null) {
        return "";
    } else {
        return results[1];
    }
}