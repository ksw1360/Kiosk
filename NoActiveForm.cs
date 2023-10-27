using System.Windows.Forms;

namespace Kiosk
{
    public partial class NoActiveForm : Form
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// WS_EX_NOACTIVATE
        /// </summary>
        private const long WS_EX_NOACTIVATE = 0x8000000L;

        /// <summary>
        /// WM_NCMOUSEMOVE
        /// </summary>
        private const int WM_NCMOUSEMOVE = 0xa0;

        /// <summary>
        /// WM_NCLBUTTONDOWN
        /// </summary>
        private const int WM_NCLBUTTONDOWN = 0xa1;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Protected

        #region 매개 변수 생성 - CreateParams

        /// <summary>
        /// 매개 변수 생성
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }
            /*
            get
            {
                CreateParams pCreateParams = base.CreateParams;

                pCreateParams.ExStyle = pCreateParams.ExStyle | (int)WS_EX_NOACTIVATE;

                return pCreateParams;
            }
            */
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Public
        /// <summary>
        /// 
        /// </summary>
        public NoActiveForm()
        {
            InitializeComponent();
        }
    }
}
