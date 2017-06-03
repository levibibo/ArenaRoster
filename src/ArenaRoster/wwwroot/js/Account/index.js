new Vue({
    el: "#user-teams",

    data: {
        
    },

    methods: {

    },

    created: function () {
        $.ajax({
            type: 'GET',
            url: 'Account/GetTeams',
            success: function (result) {
                $("#user-teams").html(result);
            },
            error: function (result) {
                $("#user-teams").html("We ran into a problem getting your teams.  Please try again later.")
                console.log(result);
            }
        })
    }
})