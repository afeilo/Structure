# Structure
开始维护一个框架
- 8.27

1.开始着手一个练手框架的编写，首先计划完成AB加载部分，今天测试AB加载策略。
- 8.28

1.AB加载策略编写，依赖加载
- 8.29

1.开始写AB引用计数，开始设计一个回收策略

- 8.31

1.重新制定回收策略，取消AB.Unload(false)操作，改为回收的时候调用AB.Unload(true)，这样才能回收彻底，不过保存了AB的实力在内存中。以后优化
Q：
如何做才能区分好Prefab,Texture等，现在的做法是对Prefab进行直接以及，对于Prefab依赖的进行间接计数。当某项计数等于0的时候，它的依赖计数-1

- 9.21

1.对比了一些框架，关于页面切换的一些思考

- 9.22

1.梳理了一下逻辑，重构。

- 9.25

1.整理一下缓存池的思路。

- 9.26

1.继续完善细化资源加载。

- 10.12

1.重新构建层级结构，[参考](https://github.com/EllanJiang/GameFramework)