namespace Stahli2Robots
{
    public enum ROBOT_INDEXES
    {
        ENUM_LOAD_ROBOT,
        ENUM_UNLOAD_ROBOT
    }
    public enum GetSend_str
    {
        ENUM_GET,
        ENUM_SEND,
        ENUM_CLEAR
    }
    public enum ProcessStatus
    {
        Stop,
        Running,
        Pause,
        CleanLine,
        SetupSucces
    }
    public enum CAMERA_NUMBER
    {
        cLoadTrayCAM,
        cLoadCarrierCAM,
        cUnloadCarrierCAM,
        cUnloadTrayCAM
    }
    public enum INSERT_SYMETRY
    {
        SQUARE,
        TRIANGLE,
        RECTENGLE,
        DAIMOND,
        NO_SYMETRY
    }

    public enum LogType
    {
        SystemErr,
        GeneralErr,
        InsertMeasure,
        Production      
    }
    public enum LogStation
    {
        LoadConv,
        UnloadConv,
        IndexTable,
        Vision,
        LoadRobot,
        UnloadRobot,
        General
    }

    public enum FrmTitleData
    {
        AreaIndexLoadTray,
        AreaIndexLoadCarrier,
        AreaSliceUnloadCarrier,
        AreaIndexUnloadTray,
        CounterPerOrder,
        InsQnt,
        lblStahliMsg,
        lblLoadConvMsg,
        lblIndexTableMsg,
        lblUnloadConvMsg,
        lblRobotsMsg,
        lblCamerasMsg,
        StepLoad,
        StepUnload,
        LoadedInsCounter,
        UnloadedInsCounter,
        MissedUnloadCount,
        MissedLoadCount,
        Robot1CycleTime,
        Robot2CycleTime,
        CarrierCycleTime,
        lblMsg,
        LoadCarrAddedX,
        LoadCarrAddedY,
        LoadAddedAngle,
        UnloadAddedAngle,
        UnloadTrayXoffset,
        UnloadTrayYoffset,
        ShowStatusImg,
        Cam1ClearToSnap,
        Cam2ClearToSnap,
        Cam3ClearToSnap,
        LoadDoorState,
        UnloadDoorState,
        RLconnbusy,
        robotdataSnap1,
        robotdataSnap2,
        RUconnbusy,
        robotdata2Snap3,
        measureResult
    }
    public enum FrmAssembliesData
    {
        LoadConvErrMsg,
        UnloadConvErrMsg,
        IndexTableErrMsg
    }
    public enum VisionActionType
    {
        ImageAcquisition,
        RunTool,
        AutoPreScan,
        AutoMissedScan
    }
    public enum FrmRobotData
    {
        CurrHight1,
        CurrHight2,
        InputState1,
        InputState2
    }
    public enum RamzorColor
    {
        None,
        Green,
        Yellow,
        Red,
        RedOff      
    }

    public enum SliceStat
    {
        NoPalet=0,          //gray
        NoCarrier=1,        //Orange
        EmptyCarrier=2,     //Yellow
        UnmachineCarrier=3, //Green minus
        MachineCarrier=4,   //Green
        Reserved=5,         //Red
        WrongDetect=6,      //OrangeX
        ByPass=7,           //Blue
        Stone=8             //light blue             
    }
}
