'use client'

import { Button, Dropdown, DropdownDivider, DropdownItem } from 'flowbite-react'
import React from 'react'
import Link from 'next/link'
import { User } from 'next-auth'
import { usePathname, useRouter } from 'next/navigation'
import { HiCog, HiUser } from 'react-icons/hi2'
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai'
import { signOut } from 'next-auth/react'
import { useParamsStore } from '../hooks/useParamsStore'

type Props = {
  user: User
}

function UserActions({user}: Props) {
  const router = useRouter();
  const pathName = usePathname();
  const setParams = useParamsStore(state => state.setParams);

  function setWinner() {
    setParams({winner: user.username, seller: undefined})
    if (pathName !== '/') router.push('/')
  }

  function setSeller() {
    setParams({seller: user.username, winner: undefined})
    if (pathName !== '/') router.push('/')
  }

  return (
    <Dropdown inline label={`Welcom ${user.name}`}>
      <DropdownItem icon={HiUser} onClick={setSeller}>
        My Auctions
      </DropdownItem>
      <DropdownItem icon={AiFillTrophy} onClick={setWinner}>
        Auctions won
      </DropdownItem>
      <DropdownItem icon={AiFillCar}>
        <Link href='/auctions/create'>
          Sell my car
        </Link>
      </DropdownItem>
      <DropdownItem icon={HiCog}>
        <Link href='/session'>
          Session (dev only!)
        </Link>
      </DropdownItem>
      <DropdownItem icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})}>
        Sign out
      </DropdownItem>
      <DropdownDivider />
    </Dropdown>
  )
}

export default UserActions