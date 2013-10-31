$(document).ready(function() {
    $("#UserInfoLink").click(function () {
        
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/ajax/get?userkey=user-2222"
        }).done(function (data, textStatus, jqXHR) {

            $("#first-name").html(data.FirstName);
            $("#last-name").html(data.LastName);
            $("#favorite-skus").html(data.FavoriteSkus);
            $("#cart-id").html(data.CartId);
            $("#cart-items").html(data.CartItems);
            $("#last-login").html(data.LastLogin);
            
        }).fail(function (error, textStatus, errorThrown) {
            alert(error);
        });

        $("#user-info").css("display", "inline");
        $("#user-info").css("padding", "4px 4px 4px 4px");
        $("#user-info").css("background-color", "lightgray");
        $("#user-info").animate(
            {
                height: "300",
                width: "600"
            }, 300);
    });
});