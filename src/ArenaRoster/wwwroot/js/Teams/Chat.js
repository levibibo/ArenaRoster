new Vue({
    el: "#message-screen",

    data: {
        teamId: "",
        teamName: "",
        playerId: "",
        playerName: "",
        readMessages: [],
        unreadMessages: [],
        messageDelay: 30000,
    },

    methods: {
        CheckMessages: function () {
            var self = this;
            self.unreadMessages.forEach(function (message) {
                self.readMessages.push(message);
            })
            self.unreadMessages.length = 0;
            console.log("checked");
            $.ajax({
                type: "GET",
                url: "/Teams/GetJsonMessages/" + self.teamId,
                success: function (response) {
                    if (response != undefined && response != "") {
                        var jsonData = JSON.parse(response);
                        jsonData.forEach(function (message) {
                            var hasBeenRead = false;
                            self.readMessages.forEach(function (readMessage) {
                                if (readMessage.Id === message.Id) {
                                    hasBeenRead = true;
                                }
                            })

                            if (!hasBeenRead) self.unreadMessages.push(message);
                        })
                    } else {
                        $("#message-window").text("No Chat Messages.\nStart a conversation!");
                    }

                    //Reset message delay if new messages have come in
                    if (self.unreadMessages.length > 0) {
                        self.messageDelay = 30000;
                    } else {
                        self.messageDelay += 30000;
                    }

                    //Recursive message check
                    setTimeout(self.CheckMessages, self.messageDelay);
                },
                error: function () {
                    $("#message-window").text("Error retrieving messages.");
                }
            })
        }
    },

    created: function () {
        var team = $("#message-window").attr("data-content");
        if (team != undefined && team != "") {
            var jsonData = JSON.parse(team);
            console.log(jsonData);
            this.teamId = jsonData.teamId;
            this.teamName = jsonData.teamName;
            this.playerId = jsonData.playerId;
            this.playerName = jsonData.playerName;
            this.CheckMessages();
        }
    }
})