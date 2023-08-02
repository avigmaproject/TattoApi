using TattoAPI.Models.Avigma;

namespace TattoAPI.IRepository
{
    public interface IQRCodeGenerator
    {
        public List<dynamic> GenerateQRImage(QRCodeModelDTO qRCodeModelDTO);
    }
}
