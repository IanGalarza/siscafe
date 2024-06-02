using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using SistemaDeVentasCafe.CodigoRepetido;
using SistemaDeVentasCafe.Models;
using QRCoder;


namespace SistemaDeVentasCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedioDeCobroController : Controller
    {
        public readonly DbapiContext _dbapiContext;

        public MedioDeCobroController(DbapiContext Context) 
        {
            _dbapiContext = Context;
        }

        //Registrar cobro con codigo QR
        [HttpGet]
        [Route("RegistrarCobroConCodigoQR")]

        public IActionResult codigoQR() {
            try
            {
              //  cob.Descripcion = "Codigo QR";
              //     Factura fac = _dbapiContext.Facturas.Find(cob.NumeroFactura);
              //    fac.EstadoPago = true;
              //  _dbapiContext.Facturas.Update(fac);
              //  _dbapiContext.Cobranzas.Update(cob);

                Mediodepago cod = new Mediodepago();
                cod.Descripcion = "Pago Realizado con Codigo QR.";
                _dbapiContext.Mediodepagos.Add(cod);
                _dbapiContext.SaveChanges();

                //Se genera el codigo QR

                     QRCodeGenerator qrGenerator = new QRCodeGenerator();
                     QRCodeData qRCodeData = qrGenerator.CreateQrCode(cod.Descripcion, QRCodeGenerator.ECCLevel.Q);
                     PngByteQRCode qrCode = new PngByteQRCode(qRCodeData);
                     byte[] qrCodeImage = qrCode.GetGraphic(20);
                     string model = Convert.ToBase64String(qrCodeImage);


                //Retorna el model que tiene la imagen del codigo QR en base 64.

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = model });
               
                    //return View("CODIGO QR", model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Por el momento no se guarda la informacion de la tarjetas, pero se podria implementar.

        [HttpPost]
        [Route("RegistrarCobroConTarjetaDeDebito")]

        public IActionResult tarjetaDebito([FromBody] TarjetaGenerica tarjeta)
        {
            try
            {
                Mediodepago cod = new Mediodepago();
                cod.Descripcion = "Pago Realizado con Tarjeta De Debito.";
                _dbapiContext.Mediodepagos.Add(cod);
                _dbapiContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = "Medio de Pago registrado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RegistrarCobroConTarjetaDeCredito")]

        public IActionResult tarjetaCredito([FromBody] TarjetaGenerica tarjeta)
        {
            try
            {
                Mediodepago cod = new Mediodepago();
                cod.Descripcion = "Pago Realizado con Tarjeta De Debito.";
                _dbapiContext.Mediodepagos.Add(cod);
                _dbapiContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = "Medio de Pago registrado con exito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }







    }
}
