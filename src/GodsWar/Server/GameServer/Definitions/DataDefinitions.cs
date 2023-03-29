using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Definitions
{
    public enum Opcode
    {
        // ÍøÂçÁ¬½Ó¹¦ÄÜÏûÏ¢
        _MSG_INVALID = 0,

        //client<->login server
        _MSG_LOGIN = 1,
        _MSG_LOGIN_RETURN_INFO,                   //µÇÂ½·þÎñÆ÷·µ»Ø
        _MSG_REQUEST_GAMESERVER,                   //ÇëÇóÓÎÏ··þÎñÆ÷

        //loginserver<--->gamereserser
        _MSG_VALIDATE_GAMESERVER = 300,              //ÓÎÏ··þÎñÆ÷ÑéÖ¤Âë

        //client<->game server
        _MSG_LOGIN_GAMESERVER = 10000,           //µÇÂ¼ÓÎÏ··þÎñÆ÷
        _MSG_RESPONSE_GAMESERVER,                   //·µ»ØÑ¡ÔñÓÎÏ··þÎñÆ÷IP
        _MSG_ROLE_INFO,                   //½ÇÉ«ÐÅÏ¢
        _MSG_CREATE_ROLE,                   //´´½¨½ÇÉ«
        _MSG_DELETE_ROLE,                   //É¾³ý½ÇÉ«
        _MSG_GAMESERVER_READY,                   //ÓÎÏ··þÎñÆ÷¾ÍÐ÷
        _MSG_ENTER_GAME,					 //¿Í»§¶Ë¾ÍÐ÷,×¼±¸½øÈëÓÎÏ·
        _MSG_CLIENT_READY,                   //¿Í»§¶Ë³õÊ¼»¯Íê±Ï
        _MSG_GAMESERVER_INFO,

        // ÐÅÏ¢¹¦ÄÜÏûÏ¢
        _MSG_SELFINFO,				     //×Ô¼ºµÄÐÅÏ¢
        _MSG_OBJECTINFO,
        _MSG_LEAVE,
        _MSG_COLONY_LEAVE,                   //ÈºÌåÏûÍö

        // ÓÎÏ·¹¦ÄÜÏûÏ¢
        _MSG_WALK_BEGIN,
        _MSG_WALK_END,
        _MSG_SCENE_CHANGE,

        // Õ½¶·¹¦ÄÜÏûÏ¢
        _MSG_FIGHT,
        _MSG_ATTACK,
        _MSG_DEAD,
        _MSG_BACKHOME,
        _MSG_DROPS,
        _MSG_UPGRADE,

        // ×°±¸µÀ¾ß¹¦ÄÜÏûÏ¢
        _MSG_KITBAG,
        _MSG_STORAGE,

        // ½»»¥¹¦ÄÜÏûÏ¢
        _MSG_TALK,
        _MSG_TALKCHANNEL,
        //_MSG_TRADE					 ,

        // ·þÎñ¶Ë²ÎÊý£¬ÏµÊý
        _MSG_PARAMATER,
        // ¼¼ÄÜ¹¦ÄÜÏûÏ¢
        _MSG_SKILL,
        _MSG_ACTIVESKILL_INFO,
        _MSG_PASSIVESKILL_INFO,
        _MSG_SELFPROPERTY,
        _MSG_EFFECT,
        _MSG_MAGIC_DAMAGE,
        _MSG_MAGIC_PERFORM,
        _MSG_MAGIC_CLUSTER_DAMAGE,


        // Ö°Òµ¹¦ÄÜÏûÏ¢
        _MSG_LEARN,                   //Ñ§Ï°¼¼ÄÜ
        _MSG_SKILL_UPGRADE,                   //¼¼ÄÜÉý¼¶

        _MSG_PICKUPDROPS,					 //Ê°È¡
        _MSG_USEOREQUIP,					 //Ê¹ÓÃ»ò×°±¸
        _MSG_MOVEITEM,					 //ÒÆ¶¯ÎïÆ·
        _MSG_BREAK_ITEM,					 //²ð·ÖÎïÆ·
        _MSG_STORAGEITEM,					 //´æ´¢ÎïÆ·
        _MSG_SELL,					 //ÂôÎïÆ·

        _MSG_STALL,					 //°ÚÌ¯
        _MSG_STALLADDITEM,					 //Ìí¼ÓÎïÆ·
        _MSG_STALLDELITEM,					 //„h³ýÎïÆ·
        _MSG_STALLITEM,					 //°ÚÌ¯ÎïÆ·
        _MSG_STALLBUYITEM,					 //Âò

        _MSG_TALKNPC,					 //NPC¶Ô»°
        _MSG_NPCDATA,					 //NPCÊý¾Ý
        _MSG_SYS_NPC_DATA,	    		     //ÏµÍ³NPCÊý¾Ý
        _MSG_SYS_FUN_USE,                   //ÏµÍ³¹¦ÄÜÊ¹ÓÃ
        _MSG_NPCITEMDATA,					 //NPC··ÂôÊý¾Ý
        _MSG_NPCSTORAGEDATA,					 //NPC²Ö¿âÊý¾Ý
        _MSG_NPCSELL,					 //NPC··Âô

        //ÈÎÎñ
        _MSG_NPCQUEST,					 //ÈÎÎñ
        _MSG_NPCNEXTQUEST,					 //ºóÐøÈÎÎñ
        _MSG_NPCQUESTS,					 //ÈÎÎñÁÐ±í
        _MSG_NPCQUESTSAVAILABLE,					 //ÈÎÎñË¢ÐÂ£¬¿É½Ó
        _MSG_NPCQUESTSUNAVAILABLE,					 //ÈÎÎñË¢ÐÂ£¬²»¿É½Ó
        _MSG_NPCQUESTREWARD,					 //ÈÎÎñ±¨³ê
        _MSG_NPCQUESTVIEW,					 //²é¿´ÈÎÎñÐÅÏ¢
        _MSG_NPCACCEPTQUEST,					 //½ÓÊÜÈÎÎñ
        _MSG_NPCCANCELQUEST,					 //È¡ÏûÈÎÎñ
        _MSG_NPCCOMPLETEQUEST,					 //Íê³ÉÈÎÎñ
        _MSG_NPCQUESTFAILD,					 //ÈÎÎñÊ§°Ü
        _MSG_NPCREWARDQUEST,					 //Íê³ÉÈÎÎñ±¨³ê
        _MSG_NPCQUESTKILLORCAST,					 //Í¬²½É±¹ÖÊÕ¼¯
        _MSG_PLAYER_ACCEPTQUESTS,					 //Í¬²½½ÓÊÜÈÎÎñ
        _MSG_FINDQUEST,					 //²éÕÒÈÎÎñ
        _MSG_FINDQUESTRESULT,					 //²éÕÒÈÎÎñ½á¹û


        //HP,MP»Ø¸´
        _MSG_RESUNE,

        //ºÃÓÑ
        _MSG_RELATIONALL,
        _MSG_RELATION_REQUEST,
        _MSG_RELATION_RESPONSE,
        _MSG_RELATION_DELETE,
        _MSG_RELATION,

        //½»Ò×
        _MSG_TRADE,
        _MSG_TRADE_MONEY,
        _MSG_TRADE_ITEM,
        _MSG_TRADE_ADDITEM,
        _MSG_TRADE_REMOVEITEM,

        _MSG_EQUIPFORGE,
        _MSG_EQUIPFORGE_EQUIP,
        _MSG_EQUIPFORGE_MATERIAL,
        _MSG_EQUIPFORGE_EQUIPCANCEL,
        _MSG_EQUIPFORGE_MATERIALCANCEL,
        _MSG_EQUIPFORGE_CANCEL,

        //±¦Ïä×ªÅÌ by lion
        _MSG_GOLD_BOX,

        _MSG_EXPLORER_QUEST,					//Ì½Ë÷ÈÎÎñÑéÖ¤

        _MSG_GOLD_BOX_RETURN,

        _MSG_TEAM_INVITE,		//client -> server  && server -> clientÑûÇë¼ÓÈë¶ÓÎé
        _MSG_TEAM_REQUEST,
        _MSG_TEAM_INFO,
        _MSG_TEAM_ADD,		//client -> server¼ÓÈë¶ÓÎé
        _MSG_TEAM_DELETE,		//client -> server¿ª³ý¶ÓÔ±		
        _MSG_TEAM_REPLACELEADER,		//client -> server¸ü»»¶Ó³¤
        _MSG_TEAM_DISSOLVE,		//client -> server¶ÓÎé½âÉ¢
        _MSG_TEAM_LEAVE,		//client -> serverÍÑÀë¶ÓÎé
        _MSG_TEAM_TIP,		//server -> client¶ÓÎéÌáÊ¾ÏûÏ¢
        _MSG_TEAM_REJECT,		//client -> server¾Ü¾ø¼ÓÈë¶ÓÎé
        _MSG_TEAM_REFLASH,		//server -> client¸üÐÂ¶ÓÎé
        _MSG_TEAM_DESTROY,		//server -> client¶ÓÎé½âÉ¢

        _MSG_UPDATE_MP,

        //¹«»á
        _MSG_CONSORTIA_CREATE,        //´´½¨¹«»á
        _MSG_CONSORTIA_CREATE_RESPONSE,        //´´½¨ºó·µ»ØÖµ 
        _MSG_CONSORTIA_BASE_INFO,        //¹«»á»á»ù±¾ÐÅÏ¢
        _MSG_CONSORTIA_MEMBER_LIST,        //³ÉÔ±ÁÐ±í
        _MSG_CONSORTIA_INVITE,        //ÑûÇë¼ÓÈë¹«»á
        _MSG_CONSORTIA_DISMISS,        //½âÉ¢¹«»á
        _MSG_CONSORTIA_RESPONSE,        //ÏìÓ¦ÑûÇë
        _MSG_CONSORTIA_EXIT,        //ÍË³ö¹«»á
        _MSG_CONSORTIA_TEXT,        //¹«»á¹«¸æ
        _MSG_CONSORTIA_DUTY,        //ÈÎÃüÖ°Îñ
        _MSG_CONSORTIA_MEMBER_DEL,        //ÒÆ³ý³ÉÔ±

        //¼ÀÌ³
        _MSG_ALTAR_INFO,		 //¼ÀÌ³ÏûÏ¢

        //·þÎñÆ÷´íÎó
        _MSG_ERROR,
        _MSG_MANAGE_RETURN,

        //¼¼ÄÜµãÊýÉý¼¶
        _MSG_SKILLPOINT_UPGRADE,

        //Í¬²½Êý¾Ý
        _MSG_SYN_GAMEDATA,

        //×´Ì¬
        _MSG_STATUS,

        //ÅÅ¶Ó
        _MSG_LOGIN_QUEUE,

        //·þÎñÆ÷Í¨Öª
        _MSG_SERVER_NOTE,
        _MSG_SKILLBACKUP,
        _MSG_SKILL_INTERRUPT,                   //¼¼ÄÜÖÐ¶Ï

        //Server->AS
        _MSG_KEY_RETURN,                                    //GSÐ£ÑéÂë·µ»Ø
        _MSG_BAN_PLAYER,
        _MSG_CONSORTIA_LVUP,	                           //¹«»áÉý¼¶
        _MSG_ALTAR_CREATE,	                               //´´½¨¼ÀÌ³
        _MSG_ALTAR_LVUP,		                          //¼ÀÌ³Éý¼¶
        _MSG_ALTAR_OBLATION,	                          //¼ÀÌ³¹©·î

        _MSG_MALLITEMDATA,				//GameServer <---> client  ·µ»ØÓÎÏ·ÉÌ³ÇÎïÆ·ÁÐ±í
        _MSG_ASSOCIATIONITEMDATA,       //GameServer <---> client  ·µ»Ø¹¤»áÉÌ³ÇÎïÆ·ÁÐ±í
        _MSG_MALLSELL,					//GameServer <---> client  ÓÎÏ·ÉÌ³Ç··Âô
        _MSG_ASSOCIATIONSELL,			//GameServer <---> client  ¹¤»áÉÌ³Ç··Âô
        _MSG_ASSOCIATIONDISCOUNT,		//GameServer ----> client  ·þÎñÆ÷·¢¸ø¿Í»§¶ËµÄÉÌ³ÇÕÛ¿Û±í(Îª¶¯Ì¬¸Ä±äÕÛ¿Û)
        //ÉùÍû
        _MSG_CRETIT_EXCHANGE,                   //ÉùÍû¶Ò»»
        _MSG_QUESTEXPLORERRESULT,					 //
        //Ôö¼ÓÒ»¸öÎïÆ·
        _MSG_SYS_ADD_ITEM,					 //Ôö¼ÓÎïÆ·
        _MSG_SYS_DEL_ITEM,					//¼õÉÙÎïÆ·
        _MSG_COUNT,
        _MSG_TARGETINFO,
        _MSG_DELAY_EXIT,								//µ¹¼ÆÊ±ÍË³ö

        _MSG_WALK,
    }
}
