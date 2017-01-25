$(document).ready(function () {

    initBinds();

    function initBinds() {
        if ($('.remove_basket').length > 0) {
            $('.remove_basket').bind('click', removeFromBasket);
        }

        if ($(".case").length > 0) {
            $(".case").bind('click', viceVersa);
        }

        if ($('#selectall').length > 0) {
            $('#selectall').bind('click', selectAll);
        }

        if ($(".renew_btn").length > 0) {
            $(".renew_btn").bind('click', renewLoans);
        }

        if ($(".submit_comment").length > 0) {
            $(".submit_comment").bind('click', submitComment);
        }


        if ($(".cancel_reservation").length > 0) {
            $(".cancel_reservation").bind('click', cancelReservation);
        }

        if ($(".checkout_loan").length > 0) {
            $(".checkout_loan").bind('click', checkoutLoan);
        }

    }



    function checkoutLoan() {
        
        var reservation = $('#checkout').attr('rel');
        var item = reservation.split("_");
        
        $.ajax({
            type: 'POST',
            url: '/mod/checkout.php',
            data: ({reservation: item[0], item: item[1], user: item[2]}),
            success: function (data) {
                alert("Item was successfully checkout and email sent to eligible waiting borrower");
                redirect("/library/");
            },
            error: function (data) {
                alert("An error has occurred");
            }
        });
        return false;
        
    }

    function redirect(url) {
        window.location = url;
    }

    function cancelReservation() {
        
        var reservation = $('#cancel').attr('rel');
        
        // Ask to confirm cancelation
        if (confirm('Are you sure you want to cancel this reservation?')) {
            $.ajax({
                type: 'POST',
                url: '/mod/cancel_reservation.php',
                data: ({reservation: reservation}),
                success: function (data) {
                    showCancelationMessage(reservation);
                },
                error: function (data) {
                    alert("An error has occurred");
                }
            });
            return false;
        }
    }
    
    function showCancelationMessage(reservation) {
        $.ajax({
            type: 'POST',
            url: '/mod/cancelation_view.php',
            dataType: 'html',
            data: ({reservation: reservation}),
            success: function (data) {
                $('#reservation_details').html(data);
                initBinds();
            },
            error: function (data) {
                alert('An error has occurred');
            }
        });
        
    }
    
    function submitComment() {
        
        var comment = $('#comments').val();

        if(comment){
            
            var reservation = $('#comments').attr('rel');
            
            $.ajax({
                type: 'POST',
                url: '/mod/submit_comment.php',
                data: ({reservation: reservation, comment:comment}),
                success: function (data) {
                     refreshReservation(reservation);
                },
                error: function (data) {
                    alert("An error has occurred");
                }
            });
            return false;
            
        }else{
            alert('Please do not provide empty comment.');
        }

    }
    
    
    function refreshReservation(reservation) {

        $.ajax({
            type: 'POST',
            url: '/mod/reservation_view.php',
            dataType: 'html',
            data: ({reservation: reservation}),
            success: function (data) {
                $('#reservation_details').html(data);
                initBinds();
            },
            error: function (data) {
                alert('An error has occurred');
            }
        });
        
        
    }

    function selectAll() {
        // add multiple select / deselect functionality
        $('.case').attr('checked', this.checked);
    }

    // if all checkbox are selected, check the selectall checkbox
    // and viceversa
    function viceVersa() {
        if ($(".case").length == $(".case:checked").length) {
            $("#selectall").attr("checked", "checked");
        } else {
            $("#selectall").removeAttr("checked");
        }
    }

    function removeFromBasket() {
        var item = $(this).attr('rel');
        $.ajax({
            type: 'POST',
            url: '/basket/RemoveItem',
            dataType: 'html',
            data: ({id: item}),
            success: function () {
                refreshBigBasket();
                refreshSmallBasket();
            },
            error: function () {
                alert("An error has occurred");
            }
        });
    }

    function refreshSmallBasket() {

        $.ajax({
            url: '/Basket/SmallRefresh',
            dataType: 'json',
            success: function (data) {

                $.each(data, function (k, v) {
                    $("#basket_left ." + k + " span").text(v);
                });
            },
            error: function (data) {
                alert("An error has occurred");
            }
        });
    }


    if ($(".add_to_basket").length > 0) {
        $(".add_to_basket").click(function () {

            var trigger = $(this);
            var param = trigger.attr("rel");
            var item = param.split("_");
            $.ajax({
                type: 'POST',
                url: '/Basket/Add',
                dataType: 'json',
                data: ({id: item[0], job: item[1]}),
                success: function (data) {
                    var new_id = item[0] + '_' + data.job;
                    if (data.job != item[1]) {
                        if (data.job == 0) {
                            trigger.attr("rel", new_id);
                            trigger.text("Remove from basket");
                            trigger.addClass("red");
                        } else {
                            trigger.attr("rel", new_id);
                            trigger.text("Add to basket");
                            trigger.removeClass("red");
                        }
                        refreshSmallBasket();
                    }
                },
                error: function (data) {
                    alert("An error has occurred");
                }
            });
            return false;
        });
    }


    function refreshBigBasket() {
        $.ajax({
            url: '/Basket/BasketView',
            dataType: 'html',
            success: function (data) {
                $('#big_basket').html(data);
                initBinds();
            },
            error: function (data) {
                alert('An error has occurred');
            }
        });

    }

    function renewLoans() {

        if ($(".case:checked").length > 0) {

            var myCheckboxes = new Array();

            $(".case:checked").each(function () {
                myCheckboxes.push($(this).val());
            });

            $.ajax({
                type: 'POST',
                url: '/mod/renew_loan.php',
                data: ({loans: myCheckboxes}),
                success: function (data) {
                    refreshLoan();
                },
                error: function (data) {
                    alert("An error has occurred");
                }
            });
            return false;

        } else {
            alert("Nothing selected");
        }
    }


    function refreshLoan() {

        $.ajax({
            url: '/mod/renew_loan_view.php',
            dataType: 'html',
            success: function (data) {
                $('#loan_list').html(data);
                initBinds();
                alert("Renew has completed successfully");
            },
            error: function (data) {
                alert('An error has occurred');
            }
        });
    }
});
