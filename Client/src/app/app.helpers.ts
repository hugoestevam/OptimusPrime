declare var jQuery: any;

export function correctHeight() {

    const pageWrapper = jQuery('#page-wrapper');
    const navbarHeigh = jQuery('nav.navbar-default').height();
    const wrapperHeigh = pageWrapper.height();

    if (navbarHeigh > wrapperHeigh) {
        pageWrapper.css('min-height', navbarHeigh + 'px');
    }

    if (navbarHeigh < wrapperHeigh) {
        if (navbarHeigh < jQuery(window).height()) {
            pageWrapper.css('min-height', jQuery(window).height() + 'px');
        } else {
            pageWrapper.css('min-height', navbarHeigh + 'px');
        }
    }

    if (jQuery('body').hasClass('fixed-nav')) {
        if (navbarHeigh > wrapperHeigh) {
            pageWrapper.css('min-height', navbarHeigh + 'px');
        } else {
            pageWrapper.css('min-height', jQuery(window).height() - 60 + 'px');
        }
    }
}

export function detectBody() {
    if (jQuery(document).width() < 769) {
        jQuery('body').addClass('body-small');
    } else {
        jQuery('body').removeClass('body-small');
    }
}

export function smoothlyMenu() {
    if (!jQuery('body').hasClass('mini-navbar') || jQuery('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        jQuery('#side-menu').hide();
        // For smoothly turn on menu
        setTimeout(
            function () {
                jQuery('#side-menu').fadeIn(400);
            }, 200);
    } else if (jQuery('body').hasClass('fixed-sidebar')) {
        jQuery('#side-menu').hide();
        setTimeout(
            function () {
                jQuery('#side-menu').fadeIn(400);
            }, 100);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        jQuery('#side-menu').removeAttr('style');
    }
}
