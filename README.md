# 3D_AOI
传送门：https://github.com/qq362946/AOI
本项目是在初见的AOI（2D）的基础上改成3D的AOI。感谢初见大佬!!!

一、算法实现。
采用十字链表+快慢针方式的AOI算法（支持ET框架）,并针对GC问题进行了优化。

性能测试、2W人在同AOI场景下，每次更新AOI耗时平均在3MS以内。插入在6MS以内。

理论上10W人或者更多效率不会下降、具体没有测试，因为不可能有一个场景下管理这么多玩家的情况。

使用说明:

首次进入AOI需要用到AoiComponent.Enter方法，里面有四个参数（Id，x坐标,y坐标,z坐标）。

角色位置变动需要用到AoiComponent.Update方法，里面有五个参数（Id，AOI范围,x坐标,y坐标,z坐标）。

AOI范围：是一个Vector3类型,x代表x轴的距离，y代表y轴的距离,z代表z轴的距离。该ID统计的数据不会超过这个距离。

操作例子：

var aoi = new AoiComponent();

var role1 = aoi.Enter(1, 12, 8,5);

Console.WriteLine($"玩家一ID:{role1.Id}");

var role2 = aoi.Enter(2, 12, 8,5);

Console.WriteLine($"玩家二ID:{role2.Id}");

aoi.Update(2, new Vector3(1, 1, 1), 13, 8,5); // 玩家二移动

Console.WriteLine($"玩家二周围列表");

foreach (var aoiNode in role2.AoiInfo.MovesSet) { Console.WriteLine(aoi.GetNode(aoiNode).Position); }

Console.WriteLine($"玩家二进入列表");

foreach (var aoiNode in role2.AoiInfo.EntersSet) { Console.WriteLine(aoi.GetNode(aoiNode).Position); }

Console.WriteLine($"玩家二离开列表");

foreach (var aoiNode in role2.AoiInfo.LeavesSet) { Console.WriteLine(aoi.GetNode(aoiNode).Position); }

Console.WriteLine($"玩家二移动列表");

foreach (var aoiNode in role2.AoiInfo.MoveOnlySet) { Console.WriteLine(aoi.GetNode(aoiNode).Position); }

二、如何使用十字链表

首先，根据场景中所有角色的x坐标排序，将其放入一个链表x_list中；
然后，根据场景中所有角色的y坐标排序，将其放入一个链表y_list中；
再接着根据场景中所有角色的z坐标排序，将其放入一个链表z_list中；
这样，所有的角色同时位于三个链表中。

1 进入
玩家进入时，根据x、y、z坐标排序，分别插入到x_list,y_list,z_list中。
同时，根据可视距离，得到x_list中可视的角色集合x_set，y_set, z_set中可视的角色集合y_list,z_list
那么(x_set & y_set & z_set)就是真正可视的角色集合，向其发送add消息

2 移动
根据角色之前的位置可以得到old_set；
移动之后，需要根据新的x、y、z坐标，重新找到角色在x_list，y_list，z_list中的位置，
然后的到新的可见角色集合为new_set，则
向(old_set - new_set)集合中玩家发送leave消息；
向(new_set - old_set)集合中玩家发送add消息；
向（old_set & old_set）集合中玩家发送move消息；z

3 离开
向当前真正可视的角色发送levea消息，然后从x_list、y_list及z_list中删除即可。

缺点，每次移动需要重新更新角色在链表中的位置，浪费CPU





