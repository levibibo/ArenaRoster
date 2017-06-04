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
        messagesReceived: false,
        messageToSend: ""
    },

    methods: {
        CheckMessages: function () {
            var self = this;
            self.unreadMessages.forEach(function (message) {
                self.readMessages.push(message);
            })
            self.unreadMessages.length = 0;
            $.ajax({
                type: "GET",
                url: "/Teams/GetMessages/" + self.teamId,
                success: function (response) {
                    self.messagesReceived = true;
                    if (response !== undefined && response !== "{}") {
                        var jsonData = JSON.parse(response);
                        if (jsonData.readMessages) {
                            $("message-window").scrollTop = $("message-window").scrollHeight;
                            self.readMessages = self.sortMessages(jsonData.readMessages);
                        }
                        if (jsonData.unreadMessages) {
                            $("message-window").scrollTop = $("message-window").scrollHeight;
                            self.unreadMessages = self.sortMessages(jsonData.unreadMessages);
                        }
                    } else {
                        $("#message-window").text("There are no chat messages for this team.\nStart a conversation!");
                    }

                    //Reset message delay if new messages have come in
                    if (self.unreadMessages.length > 0) {
                        self.messageDelay = 30000;
                    } else if (self.messageDelay < 300000) {
                        self.messageDelay += 30000;
                    }

                    //Recursive message check
                    setTimeout(self.CheckMessages, self.messageDelay);
                },
                error: function () {
                    $("#message-window").text("Error retrieving messages.");
                }
            })
        },

        sendMessage: function () {
            var self = this;
            if (self.messageToSend) {
                $.ajax({
                    type: 'POST',
                    url: '/Teams/PostMessage/' + self.teamId,
                    data: { message: self.messageToSend },
                    datatype: 'json',
                    success: function (response) {
                        self.messageToSend = "";
                        self.CheckMessages();
                    },
                    error: function (response) {
                        $("#message-window").text("Unable to send message.");
                        setTimeout(self.CheckMessages, self.messageDelay);
                    }
                })
            }
        },

        sortMessages: function (messageArray) {
            tempArray = messageArray.sort(function compare(a, b) {
                if (a.PostDateTime < b.PostDateTime) {
                    return -1;
                }
                if (a.PostDateTime > b.PostDateTime) {
                    return 1;
                }
                return 0;
            });
            return tempArray;
        }
    },

    created: function () {
        var team = $("#message-screen").attr("data-content");
        if (team !== undefined && team !== "") {
            var jsonData = JSON.parse(team);
            this.teamId = jsonData.teamId;
            this.teamName = jsonData.teamName;
            this.playerId = jsonData.playerId;
            this.playerName = jsonData.playerName;
            this.CheckMessages();
            $("message-window").scrollTop = $("message-window").scrollHeight;
        }
    }
})