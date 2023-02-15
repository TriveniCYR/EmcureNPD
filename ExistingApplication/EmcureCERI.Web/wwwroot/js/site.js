//sticky header
window.onscroll = function () { scrollFunction() };
function scrollFunction() {
  if (document.body.scrollTop > 60 || document.documentElement.scrollTop > 60) {
    $(".header").addClass('stickyTop');
  } else {
    $(".header").removeClass('stickyTop');
  }
}
//modal type - successModal, confirmModal, alertModal
function openCommonModal(modalType, modalTitle, modalMessage, returnType) {
    $('#commonModal > div').attr('class', '');
    $('#commonModal > div').addClass('modal-dialog ' + modalType);
    $('#commonModal .modal-title').text(modalTitle);
    $('#commonModal .modal-body p').text(modalMessage);
    setTimeout(function () { $('#commonModal').modal('show'); }, 50);
    if (returnType === true) {
        var d = $.Deferred();
        var isConfirmed;
        $('#commonModal').on('hide.bs.modal', function (event) {
            var $activeElement = $(document.activeElement);
            if ($activeElement.is('[data-toggle], [data-dismiss]')) {
                isConfirmed = $activeElement[0].id === "ok" ? true : false;
                d.resolve(isConfirmed);
            }
        });
        return d.promise();
    }
    else {
        return false;
    }
}


// Check if any modal open or not and take care of scroll
$(document).on('hidden.bs.modal', function (event) {
    if ($('.modal:visible').length) {
        $('body').addClass('modal-open');
    }
});

//Bootstrap select init for Language
$(document).ready(function () {
  $(function () {
    $('.langSelector').selectpicker();
  });
});

//Bootstrap tooltip
$('body').tooltip();