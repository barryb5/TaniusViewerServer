class StandardMessage {
    public MessageType messageType { get; set; }
    public double version { get; set; }

    public StandardMessage(MessageType messageType, double version) {
        this.messageType = messageType;
        this.version = version;
    }

}