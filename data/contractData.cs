class ContractData {
    public String contract {get; set; }
    public int pos {get; set; }
    public double mark {get; set; }
    public double pnl {get; set; }

    public ContractData(String contract, int pos, double mark, double pnl) {
        this.contract = contract;
        this.pos = pos;
        this.mark = mark;
        this.pnl = pnl;
    }
}