$(document).ready(function () {
    $('select').material_select();
    $('.collapsible').collapsible();
    $('.datepicker').pickadate({
        selectMonths: true,
        selectYears: 15
    });
});