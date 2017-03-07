$(document).ready(function () {
    $(".button-collapse").sideNav({
        menuWidth: '100%',
        closeOnClick: true,
        draggable: true
    });
    $('select').material_select();
    $('.collapsible').collapsible();
    $('.datepicker').pickadate({
        selectMonths: true,
        selectYears: 15
    });

});