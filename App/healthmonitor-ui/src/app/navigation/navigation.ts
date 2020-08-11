import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id: 'general',
        title: 'General',
        type: 'group',
        children: [
            {
                id: 'nodes',
                title: 'Nodes',
                type: 'item',
                icon: 'computer',
                url: '/sample'
            }
        ]
    },
    {
        id: 'namespaces',
        title: 'Namespaces',
        type: 'group',
        children: [

        ]
    }
];
