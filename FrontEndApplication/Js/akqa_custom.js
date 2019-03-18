jQuery(document).ready(function () {

    /* validate the inputs */

    jQuery("#ConvertBtn").click(function () {

        jQuery(".label-danger").hide();
        jQuery(".label-danger").text("");
       
        var number = jQuery("input[name='number'").val();
        var name = jQuery("input[name='name'").val();
        var reg = /^-?[+]?[0-9]+([.][0-9]{1,2})?$/;

        if (name == '') {

            jQuery("#labelName").text("Please enter your name");
            jQuery("#labelName").show();
            jQuery("input[name='name'").focus();
            return false;

        }

        
        if (number == "" || !reg.test(number)) {

            jQuery("#labelNumber").text("Please enter a valid Number");
            jQuery("#labelNumber").show();
            jQuery(".label-success").text('');
            jQuery(".label-success").hide();
            jQuery("input[name='number'").focus();

            return false;
        } else {
            jQuery(".label-warning").show();
            $.ajax({
                /* update the API host name */
                url: "http://akqaservices.com/api/numberconverter/" + number+"/",
                contentType: "application/json",
                dataType: "json",
                type: "get",
                success: function (resp) {
                    jQuery(".label-success").show();
                    jQuery(".label-warning").hide();
                    jQuery(".label-success").text(name + ' : "' +resp +'"');
                }
            });


        }

    });



})