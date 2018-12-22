USE [iCMSDB]
GO
CREATE FUNCTION [dbo].[F_GetDeivceByMonitorTreeId]
(
  @MonitorTreeID INT
)
RETURNS @tab TABLE(DeviceId INT)
AS
BEGIN  

  WITH    uCte
          AS ( SELECT   a.*
               FROM     dbo.Tree  a
               WHERE    MonitorTreeID = @MonitorTreeID
			   --当前节点
               UNION ALL
               SELECT   k.*
               FROM     dbo.Tree k
                        INNER JOIN uCte c ON c.MonitorTreeID = k.PID
             )
  INSERT INTO @tab  SELECT  uCte.TrueId
    FROM    uCte WHERE uCte.Level=5
  RETURN 
END
GO
/*创建取拼音首字母函数*/ 
CREATE FUNCTION [dbo].[fn_ChineseToSpell]
    (
      @strChinese VARCHAR(500) = ''
    )
RETURNS VARCHAR(500)
AS
    BEGIN /*函数实现开始*/ 
        DECLARE @strLen INT ,
            @return VARCHAR(500) ,
            @i INT; 
        DECLARE @n INT ,
            @c CHAR(1) ,
            @chn NCHAR(1);  
        SELECT  @strLen = LEN(@strChinese) ,
                @return = '' ,
                @i = 0; 
        WHILE @i < @strLen
            BEGIN /*while循环开始*/
                SELECT  @i = @i + 1 ,
                        @n = 63 ,
                        @chn = SUBSTRING(@strChinese, @i, 1); 
                IF @chn > 'z'/*原理:“字符串排序以及ASCII码表”*/
                    SELECT  @n = @n + 1 ,
                            @c = CASE chn
                                   WHEN @chn THEN CHAR(@n)
                                   ELSE @c
                                 END
                    FROM    ( SELECT TOP 27
                                        *
                              FROM      ( SELECT    chn = '吖'
                                          UNION ALL
                                          SELECT    '八'
                                          UNION ALL
                                          SELECT    '嚓'
                                          UNION ALL
                                          SELECT    '咑'
                                          UNION ALL
                                          SELECT    '妸'
                                          UNION ALL
                                          SELECT    '发'
                                          UNION ALL
                                          SELECT    '旮'
                                          UNION ALL
                                          SELECT    '铪'
                                          UNION ALL
                                          SELECT    '丌' /*because have no 'i'*/
                                          UNION ALL
                                          SELECT    '丌'
                                          UNION ALL
                                          SELECT    '咔'
                                          UNION ALL
                                          SELECT    '垃'
                                          UNION ALL
                                          SELECT    '嘸'
                                          UNION ALL
                                          SELECT    '拏'
                                          UNION ALL
                                          SELECT    '噢'
                                          UNION ALL
                                          SELECT    '妑'
                                          UNION ALL
                                          SELECT    '七'
                                          UNION ALL
                                          SELECT    '呥'
                                          UNION ALL
                                          SELECT    '仨'
                                          UNION ALL
                                          SELECT    '他'
                                          UNION ALL
                                          SELECT    '屲' /*no 'u'*/
                                          UNION ALL
                                          SELECT    '屲' /*no 'v'*/
                                          UNION ALL
                                          SELECT    '屲'
                                          UNION ALL
                                          SELECT    '夕'
                                          UNION ALL
                                          SELECT    '丫'
                                          UNION ALL
                                          SELECT    '帀'
                                          UNION ALL
                                          SELECT    @chn
                                        ) AS a
                              ORDER BY  chn COLLATE Chinese_PRC_CI_AS
                            ) AS b;  
                ELSE
                    SET @c = @chn;
                SET @return = @return + @c;  
            END; /*while循环结束*/  
        RETURN(@return);  
    END; /*函数实现结束*/
GO


