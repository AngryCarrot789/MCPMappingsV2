namespace MCPMappingsV2.Mappings.Controls {
    public interface IMainViewScroller {
        void ScrollClassToSelected();
        void ScrollFieldToSelected();
        void ScrollMethodToSelected();

        void ScrollClassToBottom();
        void ScrollFieldToBottom();
        void ScrollMethodToBottom();
    }
}
