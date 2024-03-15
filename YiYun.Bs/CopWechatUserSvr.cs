using Common;
using System;
using System.Data;
using System.Data.SqlClient;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopWechatUserSvr
    {
        /// <summary>
        /// 保存微信用户
        /// </summary>
        /// <param name="_user"></param>
        /// <param name="_resms"></param>
        /// <returns></returns>
        public static WechatUser SaveUser(WechatUser _wxuser, out string _resms)
        {
            _resms = "";
            WechatUser _reuser = new WechatUser();
            int ir = -1;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WechatUserSvr us = new WechatUserSvr(null, trans);
                            WechatUser _temp = us.GetUserByOpenId(_wxuser.OpenID);
                            //不存在则插入
                            if (_temp == null)
                            {
                                ir = us.SaveUser(_wxuser);
                                if (ir <= 0)
                                {
                                    trans.Rollback();
                                    return null;
                                }
                            }
                            //存在就更新
                            else
                            {
                                ir = us.UpdateUser(_wxuser.OpenID);
                                if (ir <= 0)
                                {
                                    trans.Rollback();
                                    return null;
                                }
                            }
                            _reuser = us.GetUserByOpenId(_wxuser.OpenID);
                            trans.Commit();
                            return _reuser;
                        }
                        catch (Exception ex0)
                        {
                            _resms = ex0.Message;
                            trans.Rollback();
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
                return null;
            }
        }

        public static WechatUser GetUserByOpenId(string _OpenID)
        {
            WechatUserSvr ws = new WechatUserSvr();
            return ws.GetUserByOpenId(_OpenID);
        }

        public static int ModifyUserAway(string _OpenID, out string _resms)
        {
            _resms = "";
            int ir = -1;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WechatUserSvr us = new WechatUserSvr(null, trans);
                            ir = us.ModifyUserAway(_OpenID);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return -1;
                            }
                            trans.Commit();
                            return 1;
                        }
                        catch (Exception ex0)
                        {
                            _resms = ex0.Message;
                            trans.Rollback();
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _resms = ex.Message;
                return -1;
            }
        }
    }
}
