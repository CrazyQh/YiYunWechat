using Common;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    /// <summary>
    /// 获取Token
    /// </summary>
    public class CopWechatTokenSvr
    {
        public static string GetToken(string _appid, string _appsecret)
        {
            int ir = 0;
            string _resms = string.Empty;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WechatTokenSvr _tokenSvr = new WechatTokenSvr(null, trans);
                            WechatToken _token = new WechatToken
                            {
                                AppID = _appid,
                                AppSecret = _appsecret
                            };
                            _token = _tokenSvr.GetToken(_token);
                            ///数据库查询
                            if (_token != null && DateTime.Parse(_token.YxDateTime) > DateTime.Now)
                            {
                                _resms = _token.Token;
                                trans.Commit();
                                return _resms;
                            }
                            ///调用接口
                            AccessTokenResult tokenresult = CommonApi.GetToken(_appid, _appsecret);
                            if (tokenresult == null)
                            {
                                trans.Rollback();
                                return null;
                            }
                            if (!tokenresult.ErrorCodeValue.Equals(0))
                            {
                                trans.Rollback();
                                return null;
                            }
                            ///默认7200秒有效时间减掉10分钟
                            _token = new WechatToken()
                            {
                                AppID = _appid,
                                AppSecret = _appsecret
                            };
                            _token.Token = tokenresult.access_token;
                            _token.YxDateTime = DateTime.Now.AddSeconds(tokenresult.expires_in - 600).ToString();
                            ///删除原来的token
                            ir = _tokenSvr.DeleteToken(_token);
                            if (ir < 0)
                            {
                                trans.Rollback();
                                return null;
                            }
                            ///插入新token
                            ir = _tokenSvr.InsertToken(_token);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return null;
                            }
                            trans.Commit();
                            return _token.Token;
                        }
                        catch (Exception e)
                        {
                            LogWrite.WriteLog(e.Message);
                            trans.Rollback();
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogWrite.WriteLog(e.Message);
                return null;
            }
        }
    }
}
