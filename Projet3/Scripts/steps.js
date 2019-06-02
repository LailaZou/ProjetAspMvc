    //jQuery time
    var current_fs, next_fs, previous_fs; //fieldsets
    var left, opacity, scale; //fieldset properties which we will animate
    var animating; //flag to prevent quick multi-click glitches

$(".next").click(function () {
        current_fs = $(this).parent();
    next_fs = $(this).parent().next();

    //activate next step on progressbar using the index of next_fs
    $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
    $(".h2").addClass("active");

    //show the next fieldset
    next_fs.fadeIn("slow");
    //hide the current fieldset with style
    current_fs.hide();
});

$(".next1").click(function () {
    current_fs = $(this).parent();
    next_fs = $(this).parent().next();
    var mdp = $("#mdp").val();
    var mdpC = $("#mdpC").val();
    if (mdp == mdpC) {
        if (mdp != "" && document.getElementById("photo").files.length != 0) {
            //activate next step on progressbar using the index of next_fs
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
            $(".h2").addClass("active");
            $("#errorMdp").text(" ");
            //show the next fieldset
            next_fs.fadeIn("slow");
            //hide the current fieldset with style
            current_fs.hide();
        }
        else { $("#errorMdp").text("Ce champ est requis"); }
    }
    else { $("#errorMdp").text("Ces champs ne sont pas identiques");}
});

$(".previous").click(function () {
        current_fs = $(this).parent();
    previous_fs = $(this).parent().prev();

    //de-activate current step on progressbar
    $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

    //show the previous fieldset
    previous_fs.fadeIn();
    current_fs.hide();
});

$(".form-control-file").readURL(this);
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#img')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

